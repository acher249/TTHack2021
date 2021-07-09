using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject
	public class LeanRotateSingleFinger : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreStartedOverGui = true;

		[Tooltip("Ignore fingers with IsOverGui?")]
		public bool IgnoreIsOverGui;

		[Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("Does rotation require an object to be selected?")]
		public LeanSelectable RequiredSelectable;

		[Tooltip("The camera we will be used to calculate relative rotations (None = MainCamera)")]
		public Camera Camera;

		[Tooltip("Should the rotation be performanced relative to the finger center?")]
		public bool Relative;

        [Tooltip("Speed scale factor")]
        public float Speed = 0.5f;

        //cwm
        public bool AllowPitch = true;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Start();
		}
#endif

		protected virtual void Start()
		{
			if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}

		protected virtual void Update()
		{
			// Get the fingers we want to use
			var fingers = LeanSelectable.GetFingersOrClear(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount, RequiredSelectable);

            //cwm
            //Use single finger horizontal to rotate
            var screenDelta = LeanGesture.GetScreenDelta(fingers);
            float rotateSpeed = Speed;
            float twistDegrees = screenDelta.x * rotateSpeed;

            float pitchSpeed = Speed;
            float pitchDegrees = screenDelta.y * pitchSpeed;



            // Calculate the rotation values based on these fingers
            //var twistDegrees = LeanGesture.GetTwistDegrees(fingers);

            if (twistDegrees != 0.0f)
			{
				if (Relative == true)
				{
					var twistScreenCenter = LeanGesture.GetScreenCenter(fingers);

					if (transform is RectTransform)
					{
						TranslateUI(twistDegrees, twistScreenCenter);
						RotateUI(twistDegrees);
					}
					else
					{
						Translate(twistDegrees, twistScreenCenter);
						Rotate(twistDegrees);
					}
				}
				else
				{
					if (transform is RectTransform)
					{
						RotateUI(-twistDegrees);
					}
					else
					{
                        //cwm- edit for single finger comtrol                         
                        if (AllowPitch)
                        {
                            if (Mathf.Abs(twistDegrees) > Mathf.Abs(pitchDegrees))
                            {
                                Rotate(-twistDegrees);
                            }
                            else
                            {
                                RotatePitch(pitchDegrees);
                            }
                        } else
                        //
                        { //
                            Rotate(-twistDegrees);
                        } //

					}
				}
			}
		}

		protected virtual void TranslateUI(float twistDegrees, Vector2 twistScreenCenter)
		{
			// Screen position of the transform
			var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position);

			// Twist screen point around the twistScreenCenter by twistDegrees
			var twistRotation = Quaternion.Euler(0.0f, 0.0f, twistDegrees);
			var screenDelta   = twistRotation * (screenPoint - twistScreenCenter);

			screenPoint.x = twistScreenCenter.x + screenDelta.x;
			screenPoint.y = twistScreenCenter.y + screenDelta.y;

			// Convert back to world space
			var worldPoint = default(Vector3);

			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true)
			{
				transform.position = worldPoint;
			}
		}

		protected virtual void Translate(float twistDegrees, Vector2 twistScreenCenter)
		{
			// Make sure the camera exists
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
				// Screen position of the transform
				var screenPoint = camera.WorldToScreenPoint(transform.position);

				// Twist screen point around the twistScreenCenter by twistDegrees
				var twistRotation = Quaternion.Euler(0.0f, 0.0f, twistDegrees);
				var screenDelta   = twistRotation * ((Vector2)screenPoint - twistScreenCenter);

				screenPoint.x = twistScreenCenter.x + screenDelta.x;
				screenPoint.y = twistScreenCenter.y + screenDelta.y;

				// Convert back to world space
				transform.position = camera.ScreenToWorldPoint(screenPoint);
			}
			else
			{
				Debug.LogError("Failed to find camera. Either tag your cameras MainCamera, or set one in this component.", this);
			}
		}

		protected virtual void RotateUI(float twistDegrees)
		{
			transform.rotation *= Quaternion.Euler(0.0f, 0.0f, twistDegrees);
		}

		protected virtual void Rotate(float twistDegrees)
		{
            var globalUp = new Vector3(0, 1, 0);			// Make sure the camera exists
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
				var axis = transform.InverseTransformDirection(globalUp);

                //transform.rotation *= Quaternion.AngleAxis(twistDegrees, axis);
                transform.Rotate(Vector3.up * twistDegrees, Space.Self);
            }
			else
			{
				Debug.LogError("Failed to find camera. Either tag your cameras MainCamera, or set one in this component.", this);
			}
		}

        //cwm
        protected virtual void RotatePitch(float pitchDegrees)
        {
            var globalUp = new Vector3(0, 1, 0);            // Make sure the camera exists
            var camera = LeanTouch.GetCamera(Camera, gameObject);

            if (camera != null)
            { 
                    transform.Rotate(Vector3.right * pitchDegrees, Space.World);
            }
            else
            {
                Debug.LogError("Failed to find camera. Either tag your cameras MainCamera, or set one in this component.", this);
            }
        }

    }
}