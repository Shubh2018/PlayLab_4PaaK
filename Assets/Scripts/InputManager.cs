using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public enum Team : byte
    {
        Team1, Team2 
    }
    
    private static InputSystem_Actions _inputActions;

    private static Vector2 _team1Rotation = Vector2.one;
    private static Vector2 _team1CannonRotation = Vector2.zero;
    private static float _shootTeam1 = 0;
    private static float _launchTeam1 = 0;
    
    private static Vector2 _team2Rotation = Vector2.one;
    private static Vector2 _team2CannonRotation = Vector2.zero;
    private static float _shootTeam2 = 0;
    private static float _launchTeam2 = 0;
    
    
    //private static 

    public static void EnableInput()
    {
        _inputActions = new InputSystem_Actions();
        
        _inputActions.Team1.Enable();
        _inputActions.Team2.Enable();
        
    #region Team1 Delegates Subscribe

        _inputActions.Team1.ShipControls_Rotation.started += ShipControls_Team1_Rotation_Started;
        _inputActions.Team1.ShipControls_Rotation.canceled += ShipControls_Team1_Rotation_Canceled;

        _inputActions.Team1.ShipControls_Cannons.performed += ShipControls_Team1_Cannons_Performed;
        _inputActions.Team1.ShipControls_Cannons.canceled += ShipControls_Team1_Cannons_Canceled;

        _inputActions.Team1.Launch.started += Launch_Team1_Started;
        _inputActions.Team1.Launch.canceled += Launch_Team1_Canceled;
        
        _inputActions.Team1.Shoot.started += Shoot_Team1_Started;
        _inputActions.Team1.Shoot.canceled += Shoot_Team1_Canceled;
        
    #endregion
        
    #region Team2 Delegates Subscribe

        _inputActions.Team2.ShipControls_Rotation.started += ShipControls_Team2_Rotation_Started;
        _inputActions.Team2.ShipControls_Rotation.canceled += ShipControls_Team2_Rotation_Canceled;
        
        _inputActions.Team2.ShipControls_Cannons.performed += ShipControls_Team2_Cannons_Performed;
        _inputActions.Team2.ShipControls_Cannons.canceled += ShipControls_Team2_Cannons_Canceled;
        
        _inputActions.Team2.Launch.started += Launch_Team2_Started;
        _inputActions.Team2.Launch.canceled += Launch_Team2_Canceled;
        
        _inputActions.Team2.Shoot.started += Shoot_Team2_Started;
        _inputActions.Team2.Shoot.canceled += Shoot_Team2_Canceled;
        
    #endregion
    }

    public static void DisableInput()
    {
    #region Team1 Delegates Unsubscribe
    
        _inputActions.Team1.ShipControls_Rotation.started -= ShipControls_Team1_Rotation_Started;
        _inputActions.Team1.ShipControls_Rotation.canceled -= ShipControls_Team1_Rotation_Canceled;
        
        _inputActions.Team1.ShipControls_Cannons.performed -= ShipControls_Team1_Cannons_Performed;
        _inputActions.Team1.ShipControls_Cannons.canceled -= ShipControls_Team1_Cannons_Canceled;
        
        _inputActions.Team1.Launch.started -= Launch_Team1_Started;
        _inputActions.Team1.Launch.canceled -= Launch_Team1_Canceled;
        
        _inputActions.Team1.Shoot.started -= Shoot_Team1_Started;
        _inputActions.Team1.Shoot.canceled -= Shoot_Team1_Canceled;
        
    #endregion
    
    #region Team2 Delegates Unsubscribe
        _inputActions.Team2.ShipControls_Rotation.started -= ShipControls_Team2_Rotation_Started;
        _inputActions.Team2.ShipControls_Rotation.canceled -= ShipControls_Team2_Rotation_Canceled;
        
        _inputActions.Team2.ShipControls_Cannons.performed -= ShipControls_Team2_Cannons_Performed;
        _inputActions.Team2.ShipControls_Cannons.canceled -= ShipControls_Team2_Cannons_Canceled;
        
        _inputActions.Team2.Launch.started -= Launch_Team2_Started;
        _inputActions.Team2.Launch.canceled -= Launch_Team2_Canceled;
        
        _inputActions.Team2.Shoot.started -= Shoot_Team2_Started;
        _inputActions.Team2.Shoot.canceled -= Shoot_Team2_Canceled;
    #endregion
        
        _inputActions.Team1.Disable();
        _inputActions.Team2.Disable();
    }

    public static float GetTeamRotationDirection(Team team)
    {
        return team == Team.Team1 ? _team1Rotation.y : _team2Rotation.y;
    }

    public static float GetTeamCannonDirection(Team team)
    {
        return team == Team.Team1 ? _team1CannonRotation.x : _team2CannonRotation.x;
    }

    public static float GetShootPressed(Team team)
    {
        return team == Team.Team1 ? _shootTeam1 : _shootTeam2;
    }

    public static float GetLaunchPressed(Team team)
    {
        return team == Team.Team1 ? _launchTeam1 : _launchTeam2;
    }
    
#region Team1 Methods

    private static void ShipControls_Team1_Rotation_Started(InputAction.CallbackContext obj)
    {
        _team1Rotation = obj.ReadValue<Vector2>();
    }
    
    private static void ShipControls_Team1_Rotation_Canceled(InputAction.CallbackContext obj)
    {
        //_team1Rotation = Vector2.zero;
    }
    
    private static void ShipControls_Team1_Cannons_Performed(InputAction.CallbackContext obj)
    {
        _team1CannonRotation = obj.ReadValue<Vector2>();
    }

    private static void ShipControls_Team1_Cannons_Canceled(InputAction.CallbackContext obj)
    {
        _team1CannonRotation = Vector2.zero;
    }
    
    private static void Launch_Team1_Started(InputAction.CallbackContext obj)
    {
        _launchTeam1 = obj.ReadValue<float>();
    }
    
    private static void Launch_Team1_Canceled(InputAction.CallbackContext obj)
    {
        _launchTeam1 = 0;
    }
    
    private static void Shoot_Team1_Started(InputAction.CallbackContext obj)
    {
        _shootTeam1  = obj.ReadValue<float>();
    }
        
    private static void Shoot_Team1_Canceled(InputAction.CallbackContext obj)
    {
        _shootTeam1 = 0;
    }
    
#endregion

#region Team2 Methods

    private static void ShipControls_Team2_Rotation_Started(InputAction.CallbackContext obj)
    {
        _team2Rotation = obj.ReadValue<Vector2>();
    }

    private static void ShipControls_Team2_Rotation_Canceled(InputAction.CallbackContext obj)
    {
        
    }
    
    private static void ShipControls_Team2_Cannons_Performed(InputAction.CallbackContext obj)
    {
        _team2CannonRotation = obj.ReadValue<Vector2>();
    }

    private static void ShipControls_Team2_Cannons_Canceled(InputAction.CallbackContext obj)
    {
        _team2CannonRotation = Vector2.zero;
    }
    
    private static void Launch_Team2_Started(InputAction.CallbackContext obj)
    {
        _launchTeam2 = obj.ReadValue<float>();
    }
    
    private static void Launch_Team2_Canceled(InputAction.CallbackContext obj)
    {
        _launchTeam2 = 0;
    }
    
    private static void Shoot_Team2_Started(InputAction.CallbackContext obj)
    {
        _shootTeam2 = obj.ReadValue<float>();
    }
    
    private static void Shoot_Team2_Canceled(InputAction.CallbackContext obj)
    {
        _shootTeam2 = 0;
    }
    
#endregion
}