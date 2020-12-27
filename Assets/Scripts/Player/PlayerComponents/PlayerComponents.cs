using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*  Не знаю, как лучше разложить компоненты между собой. Они постоянно взаимодействуют друг с другом, раньше это всё было одним скриптом */
namespace PlayerComp {
    public class PlayerComponents : MonoBehaviour
    {
        public Rigidbody2D rigidbody2d; 
        public PlayerDirection playerDirection;
        public InputPlayer inputPlayerScript;
        public Move moveScript;
		public Jump jumpScript;
        public Dash dashScript;
        public Raycasts raycasts;
        public SlowMotion slowMotion;
        public Animator animator;
        // Ok... It`s really bad script, I should use arrays...
        // TODO: rewrite for using an array instead naming scripts
        void Start()
        {
            moveScript        = gameObject.GetComponent<Move>(); 
			jumpScript		  = gameObject.GetComponent<Jump>(); 
            dashScript        = gameObject.GetComponent<Dash>();
            raycasts          = gameObject.GetComponent<Raycasts>();
            animator          = gameObject.GetComponent<Animator>();
            slowMotion        = gameObject.GetComponent<SlowMotion>();
            inputPlayerScript = gameObject.GetComponent<InputPlayer>();
			rigidbody2d		  =	gameObject.GetComponent<Rigidbody2D>();
            playerDirection   = gameObject.GetComponent<PlayerDirection>();
            // Intinalize scripts
            moveScript.DoStart();
            jumpScript.DoStart();
            dashScript.DoStart();
            raycasts.DoStart();
            slowMotion.DoStart();
        }
        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        void FixedUpdate()
        {
            dashScript.DoFixedUpdate();
            moveScript.DoFixedUpdate();
        }

        // Update is called once per frame
        void Update()
        {
            raycasts.DoUpdate();
			inputPlayerScript.DoUpdate();
            moveScript.DoUpdate();
			jumpScript.DoUpdate();
            dashScript.DoUpdate();
            slowMotion.DoUpdate();
        }
    }
    public interface IUpdate {
        void DoUpdate();
    }
	public interface IFixedUpdate {
		void DoFixedUpdate();
	}
    public interface IStart {
        void DoStart();
    }
}
