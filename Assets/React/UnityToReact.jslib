var UnityToReactPlugin = {
    OnScoreUpdate: function(score) {
        // Call a function in the parent window (React app)
        if (window.parent && window.parent.onUnityScoreUpdate) {
            window.parent.onUnityScoreUpdate(score);
        }
        // Or call a global function in the same window
        else if (window.onUnityScoreUpdate) {
            window.onUnityScoreUpdate(score);
        }
        // Fallback - log to console
        else {
            console.log("Unity Score Update:", score);
        }
    }
};

mergeInto(LibraryManager.library, UnityToReactPlugin); 