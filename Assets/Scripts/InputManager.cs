using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public enum Player : byte
    {
        None, Player1, Player2, Player3, Player4 
    }
    
    private static InputSystem_Actions _inputActions;

    private static Vector2 _player1Rotation = Vector2.one;
    private static float _launchPlayer1 = 0; 
    
    private static Vector2 _player2Rotation = Vector2.one;
    private static float _launchPlayer2 = 0;
    
    private static Vector2 _player3Rotation = Vector2.one;
    private static float _launchPlayer3 = 0;
    
    private static Vector2 _player4Rotation = Vector2.one;
    private static float _launchPlayer4 = 0;
    
    
    //private static 

    public static void EnableInput()
    {
        _inputActions = new InputSystem_Actions();
        
        _inputActions.Player1.Enable();
        _inputActions.Player2.Enable();
        _inputActions.Player3.Enable();
        _inputActions.Player4.Enable();
        
    #region Player1 Delegates Subscribe

        _inputActions.Player1.ShipControls_Rotation.started += ShipControls_Player1_Rotation_Started;

        _inputActions.Player1.Launch.started += Launch_Player1_Started;
        _inputActions.Player1.Launch.canceled += Launch_Player1_Canceled;
        
    #endregion
        
    #region Player2 Delegates Subscribe

        _inputActions.Player2.ShipControls_Rotation.started += ShipControls_Player2_Rotation_Started;
        
        _inputActions.Player2.Launch.started += Launch_Player2_Started;
        _inputActions.Player2.Launch.canceled += Launch_Player2_Canceled;
        
    #endregion
    
    #region Player3 Delegates Subscribe

        _inputActions.Player3.ShipControls_Rotation.started += ShipControls_Player3_Rotation_Started;
            
        _inputActions.Player3.Launch.started += Launch_Player3_Started;
        _inputActions.Player3.Launch.canceled += Launch_Player3_Canceled;
        
    #endregion
    
    #region Player4 Delegates Subscribe

        _inputActions.Player4.ShipControls_Rotation.started += ShipControls_Player4_Rotation_Started;
                
        _inputActions.Player4.Launch.started += Launch_Player4_Started;
        _inputActions.Player4.Launch.canceled += Launch_Player4_Canceled;
        
    #endregion
    }

    public static void DisableInput()
    {
    #region Player1 Delegates Unsubscribe
    
        _inputActions.Player1.ShipControls_Rotation.started -= ShipControls_Player1_Rotation_Started;
        
        _inputActions.Player1.Launch.started -= Launch_Player1_Started;
        _inputActions.Player1.Launch.canceled -= Launch_Player1_Canceled;
        
    #endregion
    
    #region Player2 Delegates Unsubscribe
    
        _inputActions.Player2.ShipControls_Rotation.started -= ShipControls_Player2_Rotation_Started;
        
        _inputActions.Player2.Launch.started -= Launch_Player2_Started;
        _inputActions.Player2.Launch.canceled -= Launch_Player2_Canceled;
        
    #endregion
    
    #region Player3 Delegates Unsubscribe
    
        _inputActions.Player3.ShipControls_Rotation.started -= ShipControls_Player3_Rotation_Started;
            
        _inputActions.Player3.Launch.started -= Launch_Player3_Started;
        _inputActions.Player3.Launch.canceled -= Launch_Player3_Canceled;
        
    #endregion
    
    #region Player4 Delegates Unsubscribe
    
        _inputActions.Player4.ShipControls_Rotation.started -= ShipControls_Player4_Rotation_Started;
                
        _inputActions.Player4.Launch.started -= Launch_Player4_Started;
        _inputActions.Player4.Launch.canceled -= Launch_Player4_Canceled;
        
    #endregion
        
        _inputActions.Player1.Disable();
        _inputActions.Player2.Disable();
        _inputActions.Player3.Disable();
        _inputActions.Player4.Disable();
    }

    public static float GetPlayerRotationDirection(Player Player)
    {
        return Player switch
        {
            Player.Player1 => _player1Rotation.y,
            Player.Player2 => _player2Rotation.y,
            Player.Player3 => _player3Rotation.y,
            Player.Player4 => _player4Rotation.y,
            Player.None => 0.0f
        };
    }

    public static float GetLaunchPressed(Player Player)
    {
        return Player switch
        {
            Player.Player1 => _launchPlayer1,
            Player.Player2 => _launchPlayer2,
            Player.Player3 => _launchPlayer3,
            Player.Player4 => _launchPlayer4,
            Player.None => 0.0f
        };
    }
    
#region Player1 Methods

    private static void ShipControls_Player1_Rotation_Started(InputAction.CallbackContext obj)
    {
        _player1Rotation = obj.ReadValue<Vector2>();
    }
    
    private static void Launch_Player1_Started(InputAction.CallbackContext obj)
    {
        _launchPlayer1 = obj.ReadValue<float>();
    }
    
    private static void Launch_Player1_Canceled(InputAction.CallbackContext obj)
    {
        _launchPlayer1 = 0;
    }
    
#endregion

#region Player2 Methods

    private static void ShipControls_Player2_Rotation_Started(InputAction.CallbackContext obj)
    {
        _player2Rotation = obj.ReadValue<Vector2>();
    }
    
    private static void Launch_Player2_Started(InputAction.CallbackContext obj)
    {
        _launchPlayer2 = obj.ReadValue<float>();
    }
    
    private static void Launch_Player2_Canceled(InputAction.CallbackContext obj)
    {
        _launchPlayer2 = 0;
    }
    
#endregion

#region Player3 Methods

    private static void ShipControls_Player3_Rotation_Started(InputAction.CallbackContext obj)
    {
        _player3Rotation = obj.ReadValue<Vector2>();
    }
        
    private static void Launch_Player3_Started(InputAction.CallbackContext obj)
    {
        _launchPlayer3 = obj.ReadValue<float>();
    }
        
    private static void Launch_Player3_Canceled(InputAction.CallbackContext obj)
    {
        _launchPlayer3 = 0;
    }
    
#endregion

#region Player4 Methods

    private static void ShipControls_Player4_Rotation_Started(InputAction.CallbackContext obj)
    {
        _player4Rotation = obj.ReadValue<Vector2>();
    }
            
    private static void Launch_Player4_Started(InputAction.CallbackContext obj)
    {
        _launchPlayer4 = obj.ReadValue<float>();
    }
            
    private static void Launch_Player4_Canceled(InputAction.CallbackContext obj)
    {
        _launchPlayer4 = 0;
    }
    
#endregion
}