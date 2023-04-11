using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sm = UnityEngine.SceneManagement;

public static class SceneManager {
    public enum Scene {
        MainMenu,
        Sudoku_1,
        Sudoku_2
    }

    private static Scene startScene = Scene.MainMenu;
    private static Scene _lastScene = Scene.MainMenu;
    public static Scene LastScene => _lastScene;
    public static void LoadScene(Scene scene) {
        SceneTransition.Instance.FadeOut(() => sm.SceneManager.LoadScene((int)scene));
    }

    public static void LoadStartScene() {
        GameManager.Instance.ResetValues();
        LoadScene(startScene);
        _lastScene = startScene;
    }

    public static void UnloadScene(Scene scene) {
        string logId = "SceneManager::UnloadScene";
        Utils.logd(logId, "Unloading Scene="+scene);
        asyncOperation = sm.SceneManager.UnloadSceneAsync((int)scene, sm.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
    public static void UnloadLastScene() {
        string logId = "SceneManager::UnloadLastScene";
        Utils.logd(logId, "Unloading Scene="+_lastScene);
        asyncOperation = sm.SceneManager.UnloadSceneAsync((int)_lastScene, sm.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }

    public static void LoadSceneAsync(Scene scene) {
        lastLoadedScene = scene;
        asyncOperation = sm.SceneManager.LoadSceneAsync((int)scene, sm.LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;
    }
    public static AsyncOperation asyncOperation = null;
    private static Scene lastLoadedScene;
    public static void ActivateScene(Scene scene) {
        var logId = "SceneManager::ActivateScene";
        bool asyncOperationIsDone = asyncOperation==null?false:asyncOperation.progress==0.9f;
        if(!asyncOperationIsDone || lastLoadedScene!=scene) {
            Utils.logw(logId, "AsyncOperation="+asyncOperation.logf()+" AsyncOperationIsDone="+asyncOperationIsDone+" Scene="+scene.logf()+" AsyncLoadedScene="+lastLoadedScene.logf()+" => no-op");
            return;
        }
        Utils.logd(logId, "Activating Scene="+scene);
        var sceneTransition = SceneTransition.Instance;
        Utils.logd(logId, "SceneTransition="+sceneTransition.logf());
        SceneTransition.Instance.FadeOut(() => {
            asyncOperation.allowSceneActivation = true;
            sm.SceneManager.sceneLoaded += OnSceneLoaded;
        });
    }

    private static void OnSceneLoaded(sm.Scene scene, sm.LoadSceneMode mode) {
        string logId = "SceneManager::OnSceneLoaded";
        Utils.logd(logId, "Scene="+scene.logf()+" Mode="+mode.logf());
        sm.SceneManager.SetActiveScene(scene);
        UnloadLastScene();
        Utils.logd(logId, "LastScene="+_lastScene.logf()+" LastLoadedScene="+lastLoadedScene.logf());
        _lastScene = lastLoadedScene;
        lastLoadedScene = scene==null?Scene.MainMenu:(Scene)scene.buildIndex;
        sm.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void ReloadScene() {
        LoadScene(lastLoadedScene);
    }
}