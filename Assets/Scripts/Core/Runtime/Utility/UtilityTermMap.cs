namespace Core.Runtime.Utility
{
    public static class UtilityTermMap
    {
        private const string _player = "Player";
        private const string _move = "Move";
        private const string _interact = "Interact";
        private const string _openInventory = "OpenInventory";
        private const string _leftMouseButton = "LeftMouseButton";
        private const string _rightMouseButton = "RightMouseButton";
        private const string _escape = "Escape";

        private const string _devConsole = "DevConsole";
        private const string _debug = "Debug";
        private const string _debugSpawnAt = "DebugSpawnAt";
        private const string _debugCancel = "DebugCancel";

        private const string _interactable = "Interactable";

        public static string Player => _player;
        public static string Move => _move;
        public static string Interact => _interact;
        public static string OpenInventory => _openInventory;

        public static string DevConsole => _devConsole;

        public static string Interactable => _interactable;

        public static string LeftMouseButton => _leftMouseButton;

        public static string RightMouseButton => _rightMouseButton;

        public static string Debug => _debug;

        public static string DebugSpawnAt => _debugSpawnAt;

        public static string DebugCancel => _debugCancel;

        public static string Escape => _escape;
    }
}
