using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Main generator script
/// </summary>
public class LevelGenerator :MonoBehaviour
{
    public Transform player;
    public TileBase ruleTile;
    // Random structs
    public RandomStruct randSizeX;
    public RandomStruct randPosY;
    public RandomStruct randPosX;
    public RandomStruct randPlatformCount;
    public AnimationCurve moneyPercent;
    public Vector3Int moneyOffset;
    public MoneyScript moneyPrefab1;
    public MoneyScript moneyPrefab2;
    public int maxY;
    public int minY;

    private List<Platform> platforms = new List<Platform>(1);

    private Vector3Int lastLevelPos;
    private Vector3Int startPrev;
    private Vector3Int endPrev;
    private Tilemap map;
    private int score;
    private void Start() {
        map = GetComponent<Tilemap>();
        BoundsInt area = map.cellBounds;

        BoundsInt bounds = map.cellBounds;
        // try get a position of start platform
        for (int x = 0; x < bounds.size.x; x++) {
            TileBase tile = null;
            for (int y = 0; y < bounds.size.y; y++) {
                tile =  map.GetTile(new Vector3Int(x, y, 0));
                if (tile != null) {
                    lastLevelPos.y = y;
                    lastLevelPos.x = x;
                    break;
                }
            }
            if (tile != null) {
                break;
            }
        }
        // add ofsset
        maxY += lastLevelPos.y;
        minY += lastLevelPos.y;
        // create first tiles
        lastLevelPos = GenerateLevel(lastLevelPos + new Vector3Int(4, 0, 0));
        lastLevelPos = EndTile(lastLevelPos);
    }
    /// <summary>
    /// Function for generate next level
    /// </summary>
    public void ContinueGenerate() {
        DeletePrevTiles();
        score = UIController.GetScore();
        lastLevelPos = GenerateLevel(lastLevelPos);
        lastLevelPos = EndTile(lastLevelPos);
    }
    /// <summary>
    /// Delete previous tiles
    /// </summary>
    public void DeletePrevTiles() {
        for (int i = 0; i < platforms.Count - 1; i++) {
            platforms[i].DestroyPlatform(map);
        }
        platforms.RemoveRange(0, platforms.Count - 1);
    }
    /// <summary>
    /// Generate level based on random
    /// </summary>
    /// <param name="minPos"> start pos of generate</param>
    /// <returns></returns>
    public Vector3Int GenerateLevel(Vector3Int minPos) {
        int platformsCount = randPlatformCount.CalculateRandom(score);
        for (int i = 0; i < platformsCount; i++) {
            int posOffset = randPosX.CalculateRandom(score);
            minPos = generateTile(minPos + new Vector3Int(posOffset, 0, 0));
        }
        return minPos;
    }
    /// <summary>
    /// generate a platforms based on random
    /// </summary>
    /// <param name="tilePos">start platform pos</param>
    /// <returns></returns>
    public Vector3Int generateTile (Vector3Int tilePos) {
        int rangeX = randSizeX.CalculateRandom(score);
        int rangeY = randPosY.CalculateRandom(score);
        int y = Mathf.Clamp(tilePos.y + rangeY, minY, maxY);
        Vector3Int position = new Vector3Int(tilePos.x, y, 0);
        return drawPlatform(position, rangeX);
    }
    /// <summary>
    /// draw and save platform
    /// </summary>
    /// <param name="startPos">start position of platform</param>
    /// <param name="length"> a platform length</param>
    /// <returns></returns>
    public Vector3Int drawPlatform (Vector3Int startPos, int length) {
        Platform platform = new Platform(startPos, length, ruleTile);
        platform.Draw(map);
        platforms.Add(platform);
        return startPos + new Vector3Int(length, 0, 0);
    }
    /// <summary>
    /// Tile with monety
    /// </summary>
    /// <param name="lastPos">Start position</param>
    /// <returns></returns>
    public Vector3Int EndTile (Vector3Int lastPos) {
        lastPos += new Vector3Int (3, 0, 0);
        lastPos = drawPlatform(lastPos, 5);
        Vector3 prefabPos = map.CellToWorld(lastPos + moneyOffset);
        CreateMoney(prefabPos);
        return lastPos;
    }
    /// <summary>
    /// Generate money
    /// </summary>
    /// <param name="pos">pos in world space for money</param>
    public void CreateMoney (Vector3 pos) {
        MoneyScript create = moneyPrefab1;
        if (Random.value < moneyPercent.Evaluate(platforms.Count)) {
            create = moneyPrefab2;
        }
        MoneyScript moneyScript = Instantiate(create, pos, Quaternion.identity);
        moneyScript.SetLevelGenerator(this);
    }
}
/// <summary>
/// Platform class
/// </summary>
public class Platform {
    private Vector3Int startPos;
    private int length;
    private TileBase ruleTile;
    public Platform(Vector3Int startPos, int length, TileBase ruleTile) {
        this.startPos = startPos;
        this.length   = length;
        this.ruleTile = ruleTile;
    }
    public void Draw(Tilemap map) {
        for (int i = 0; i < length; i++) {
            map.SetTile(startPos + new Vector3Int(i, 0, 0), ruleTile);
        }
    }
    public void DestroyPlatform(Tilemap map) {
        for (int i = 0; i < length; i++) {
            map.SetTile(startPos + new Vector3Int(i, 0, 0), null);
        }
    }
    public Vector3Int GetStartPos() {
        return startPos;
    }
    
}
