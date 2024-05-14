using System;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// The base class to use for Unity scripts. Contains utilities for referencing components and overriding Unity functions.
/// </summary>
public abstract class BaseBehavior : MonoBehaviour {

    #region Unity lazy-loaded property definitions
    /// <summary>
    /// This SpriteRenderer component for this GameObject, if one exists.
    /// </summary>
    protected SpriteRenderer Renderer {
        get {
            return GetComponentOrNull(ref rendererRef);
        }
    }
    private SpriteRenderer rendererRef = null;

    /// <summary>
    /// This Animator component for this GameObject, if one exists.
    /// </summary>
    protected Animator Animator {
        get {
            return GetComponentOrNull(ref animatorRef);
        }
    }
    private Animator animatorRef = null;

    /// <summary>
    /// The Camera component for this GameObject, if one exists.
    /// </summary>
    protected Camera Camera {
        get {
            return GetComponentOrNull(ref cameraRef);
        }
    }
    private Camera cameraRef = null;

    /// <summary>
    /// The PixelPerfectCamera component for this GameObject, if one exists.
    /// </summary>
    protected PixelPerfectCamera PixelPerfectCamera {
        get {
            return GetComponentOrNull(ref pixelPerfectCameraRef);
        }
    }
    private PixelPerfectCamera pixelPerfectCameraRef = null;

    protected Rigidbody2D RigidBody {
        get {
            return GetComponentOrNull(ref rigidBodyRef);
        }
    }
    private Rigidbody2D rigidBodyRef = null;
    #endregion

    #region Helpful wrapper properties
    protected Vector3 Position {
        get {
            return transform.position;
        }
        set {
            transform.position = value;
        }
    }
    #endregion

    /// <summary>
    /// Attempts to fetch a component from this GameObject, printing a warning when that component doesn't exist.
    /// </summary>
    /// <typeparam name="T">The component being fetched.</typeparam>
    /// <param name="comp">The field to store a reference to this component in.</param>
    protected T GetComponentOrNull<T>(ref T comp) where T : Component {
        if (comp == null && !TryGetComponent(out comp)) {
            Debug.LogWarning($"An reference attempt was made on null component of type {typeof(T).Name}.");
        }
        return comp;
    }

    /// <summary>
    /// Returns the number of children this object currently has.
    /// </summary>
    public int ChildCount {
        get {
            if (gameObject == null || gameObject.transform == null) {
                throw new NullReferenceException("Unable to find a game object associated with this behavior.");
            }
            return gameObject.transform.childCount;
        }
    }

    /// <summary>
    /// Deletes all children attached to this object. Does not modify the object itself.
    /// </summary>
    public void DestroyChildren() {
        if (gameObject == null || gameObject.transform == null) {
            throw new NullReferenceException("Unable to destroy children of a null object.");
        }
        for (var i = 0; i < gameObject.transform.childCount; i++) {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

    #region Unity function overrides
    // Unity awake function.
    private void Awake() {
        OnAwake();
    }

    /// <summary>
    /// Override this function to define actions to take when Unity make a call to Awake().
    /// This occurs when the <c>GameObject</c> this script is attached to is first loaded.
    /// </summary>
    protected virtual void OnAwake() { }

    // Unity start function.
    private void Start() {
        OnStart();
    }

    /// <summary>
    /// Override this function to define actions to take when Unity make a call to Start().
    /// </summary>
    protected virtual void OnStart() { }

    // Unity update function.
    private void Update() {
        OnUpdate(TimeSpan.FromSeconds(Time.deltaTime));
    }

    /// <summary>
    /// Override this function to define actions to take when Unity make a call to Update().
    /// </summary>
    /// <param name="delta">A TimeSpan representing the time in seconds since the last call of this function.</param>
    protected virtual void OnUpdate(TimeSpan delta) { }

    // Unity fixed update function.
    private void FixedUpdate() {
        OnFixedUpdate(TimeSpan.FromSeconds(Time.deltaTime));
    }

    /// <summary>
    /// Override this function to define actions to take when Unity make a call to FixedUpdate().
    /// This function is called every frame, if one is set.
    /// </summary>
    /// <param name="delta">A TimeSpan representing the time in seconds since the last call of this function.</param>
    protected virtual void OnFixedUpdate(TimeSpan delta) { }

    /// <summary>
    /// Override this function if you need to know when the collider/rigidbody of this object intersects with another.
    /// </summary>
    /// <param name="collision">Collision data for the intersecting object.</param>
    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    /// <summary>
    /// Override this function if you need to define behaviors that trigger when this object is destroyed.
    /// </summary>
    protected virtual void OnDestroy() { }
    #endregion
}