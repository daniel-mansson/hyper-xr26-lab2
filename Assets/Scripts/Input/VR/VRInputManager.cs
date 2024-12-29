using UnityEngine;
using UnityEngine.XR.Management;

public class VRInputManager : MonoBehaviour, IInputProvider
{
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private VRControllerInput leftController;
    [SerializeField]
    private VRControllerInput rightController;
    [SerializeField]
    private Transform rigTransform;

    public Transform GetRigTransform()
    {
        return rigTransform;
    }

    public Transform GetHeadTransform()
    {
        return headTransform;
    }

    public IControllerInput GetLeftController()
    {
        return leftController;
    }

    public IControllerInput GetRightController()
    {
        return rightController;
    }

    public bool TryInitialize()
    {
        var manager = XRGeneralSettings.Instance.Manager;

        if (manager.isInitializationComplete)
        {
            manager.DeinitializeLoader();
        }
        manager.InitializeLoaderSync();

        if (!manager.isInitializationComplete)
        {
            return false;
        }

        manager.StartSubsystems();
        return true;
    }

    private void Update()
    {
        OVRInput.Update();
    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }
}
