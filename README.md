# Reference Any Path in Unity

Reference any file or folder from Unity Inspector, whether it's inside or outside your Unity project, and then retrieve the file path in your code:

```csharp
public class YourBehaviour : MonoBehaviour {
    [SerializeField] AnyPath _anyPath;

    void Start() {
        var path = _anyPath.Path; // Path for use in runtime/build

#if UNITY_EDITOR
        var absolutePath = _anyPath.AbsolutePath;
        var relativePath = _anyPath.RelativePath; // Relative to Assets folder
        var assetPath = _anyPath.AssetPath; // Starts with Assets ot Packages/<package name>
        var runtimePath = _anyPath.RuntimePath; // Path for use in runtime
#endif
    }
}
```

Available types:

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class YourBehaviour : MonoBehaviour {
    [SerializeField] AnyPath _anyPathOnYourHardDrive;
    [SerializeField] AnyFile _anyFileOnYourHardDrive;
    [SerializeField] AnyFolder _anyFolderOnYourHardDrive;
    [SerializeField] RawHeightmapFile _anyRawHeightmapFile;

    [SerializeField] Asset _anyAssetInYourProject;
    [SerializeField] AssetFile _anyFileInYourProject;
    [SerializeField] AssetFolder _anyFolderInYourProject;
    [SerializeField] Asset<Material> _anyMaterialInYourProject; // See Limitations section
    [SerializeField] RawHeightmapAsset _anyRawHeightmapInYourProject;

    // Runtime Assets are Resources, Streaming Assets, scenes
    [SerializeField] RuntimeAsset _anyRuntimeAsset; 
    [SerializeField] RuntimeFile _anyRuntimeFile;
    [SerializeField] RuntimeFolder _anyRuntimeFolder;
    [SerializeField] Runtime<Material> _anyRuntimeMaterial; // See Limitations section
    [SerializeField] RawHeightmapRuntimeAsset _anyRawHeightmapRuntimeAsset; // Should have ".bytes" extension when in Resources

    [SerializeField] Resource _anyResource;
    [SerializeField] ResourceFile _anyResourceFile;
    [SerializeField] ResourceFolder _anyResourceFolder;
    [SerializeField] Resource<Material> _anyResourceMaterial; // See Limitations section
    [SerializeField] TextResource _anyTextAssetResource;
    [SerializeField] BinaryResource _anyBinaryResource; // Should have ".bytes" extension
    [SerializeField] RawHeightmapResource _anyRawHeightmapResource; // Should have ".bytes" extension

    [SerializeField] StreamingAsset _anyStreamingAsset;
    [SerializeField] StreamingAssetFile _anyStreamingAssetFile;
    [SerializeField] StreamingAssetFolder _anyStreamingAssetFolder;
    [SerializeField] TextStreamingAsset _anyTextStreamingAsset;
    [SerializeField] BinaryStreamingAsset _anyBinaryStreamingAsset;
    [SerializeField] RawHeightmapStreamingAsset _anyStreamingAssetHeightmap;

    [SerializeField] Scene _anySceneInYourProject;
}
```

## License

[MIT](LICENSE.md)

## Limitations

### Unity version requirements

**Unity 2021.2.18f1** or above is required because this package relies on both serialization of generic types and Editor-only fields support.

---

### Generics limitation

E.g. `[SerializeField] List<Resource<Material>> _material;`.

While Unity now supports generic type serialization and Editor-only fields, you can't use them this way. This code will fail with classic layout error:

```
A scripted object (probably <serialized object type>?) has a different serialization layout when loading. (Read <N> bytes but expected <M> bytes)
Did you #ifdef UNITY_EDITOR a section of your serialized properties in any of your scripts?
```

Important notes:
- sometimes the error doesn't exist
- sometimes build crashes on startup with cryptic error.

So the advice is to **not use generics within generics.**

Suggested workarounds:
- Either use non-generics, e.g. `[SerializeField] List<Resource>_material;`. 
- or subclass a generic, e.g. `public class ResourceMaterial : Resource<Material> { }` and then use it `[SerializeField] List<ResourceMaterial> _material;`.

---

### Other limitations:

<details><summary><b>Asset validation</b></summary>

`AnyPath`, `AnyFile`, `AnyFolder` and `RawHeightmapFile` are path-based. When they reference assets within your Unity project, and you move these assets, changes are not reflected.

Suggested workaround: use any other available type that is asset-based.

</details>

<details><summary><b>Resources</b></summary>

Unity doesn't use file extensions when loading Resources in runtime. This means that if you have e.g. `Text.md` and `Text.txt` files in the same folder, they both can coexist there but in runtime the path will be the same for both files.

Suggested workaround:
- Name your resources of the same type differently, or put them in a different folder.
- For resources of different types with the same name, use generics, e.g. `[SerializeField] Resource<Material> _material;`, `[SerializeField] Resource<TextAsset> _textAsset;`

</details>

<details><summary><b>Streaming Assets</b></summary>

#### Streaming Assets serialize differently

When moving assets within Unity, you can lose references if you move them to and from `StreamingAssets` folder. This is because assets in that folder don't stay the same object as when outside of it.

- When an object was referenced while stored outside `StreamingAssets` folder, and then is moved to that folder, the reference and the path survive.

- When an object was referenced while stored inside `StreamingAssets` folder, and then is moved outside that folder - it's reference doesn't survive and paths don't change.

- Exception is folders: they can be moved to and from StreamingAssets folder.

#### Folder loading is not supported

As some major platforms (Android and WebGL) don't allow the use of the .NET `File` API, listing files in folders is not supported, and bulk loading a folder is not possible. There are some known workarounds at least for Android, but they are not perfect, so this cannot be automated.

</details>

<details><summary><b>OnBeforeSerialize</b></summary>

The main issue with a system like that is that it is convenient to reacct to moving and deleting assets in Unity, for which `OnBeforeSerialize` is used. Unity is not very open on what you may or you may not use in `OnBeforeSerialize`.

The use of `AssetDatabase.GetAssetPath()` was avoided because it triggers the annoying `Objects are trying to be loaded during a domain backup. This is not allowed as it will lead to undefined behaviour!` error message but some `AssetDatabase` API is still used, so errors may return in some future Unity version.

Suggested workaround: use `REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION` Scripting Define to remove the path check and restoration code.

</details>

<details><summary><b>PropertyDrawer</b></summary>

Non-asset paths are validated in a thread, to avoid blocking the Editor UI thread for disc access. The validation starts when UI updates, and it synchronizes back when the thread completes, and when the state changes, it triggers the repaint of some Editor windows. While it should have no performance impact in most cases, there is the `REFERENCE_ANY_PATH_NO_ASYNC_CHECK_IN_INSPECTOR` Scripting Define for convenience that removes the parallel validation from `PropertyDrawer`. Note that only `AnyPath`, `AnyFile`, `AnyFolder` and `RawHeightmapFile` are affected, asset-based drawers won't benefit from this.

</details>

## Installation

- Via Unity Package Manager: press the plus sign and choose `Add package from git URL...`. There, use `https://github.com/marked-one/UnityReferenceAnyPath.git`, or, with version: `https://github.com/marked-one/UnityReferenceAnyPath#1.0.0`
- You can also clone this repository and then add it as a local package using `Add package from disk...` option.
- Another way is to manually edit the `manifest.json` file in your `Packages` folder. Add `"com.vklubkov.referenceanypath" : "https://github.com/marked-one/UnityReferenceAnyPath.git"`, or, with version: `"com.vklubkov.referenceanypath" : "hhttps://github.com/marked-one/UnityReferenceAnyPath.git#1.0.0"`
- Alternatively, you can download the package into your `Assets` folder

## Usage

<details><summary><b>AnyPath</b></summary>

![AnyPath](.github/AnyPath.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class AnyPathExample : MonoBehaviour {
    [SerializeField] AnyPath _anyPath;

    void Start() {
        var path = _anyPath.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>AnyFile</b></summary>

![AnyFile](.github/AnyFile.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class AnyFileExample : MonoBehaviour {
    [SerializeField] AnyFile _anyFile;

    void Start() {
        var path = _anyFile.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>AnyFolder</b></summary>

![AnyFolder](.github/AnyFolder.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class AnyFolderExample : MonoBehaviour {
    [SerializeField] AnyFolder _anyFolder;

    void Start() {
        var path = _anyFolder.Path;
        Debug.Log(path);
    }
}
```

</details>

Path-based references, primarily for Editor use.

---

<details><summary><b>Asset</b></summary>

![Asset](.github/Asset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class AssetExample : MonoBehaviour {
    [SerializeField] Asset _asset;

    void Start() {
        var path = _asset.Path;
        Debug.Log(path);
    }
}
```
</details>

<details><summary><b>Asset<></b></summary>

![GenericAsset](.github/GenericAsset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class GenericAssetExample : MonoBehaviour {
    [SerializeField] Asset<Material> _asset;

    void Start() {
        var path = _asset.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>AssetFile</b></summary>

![AssetFile](.github/AssetFile.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class AssetFileExample : MonoBehaviour {
    [SerializeField] AssetFile _assetFile;

    void Start() {
        var path = _assetFile.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>AssetFolder</b></summary>

![AssetFolder](.github/AssetFolder.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class AssetFolderExample : MonoBehaviour {
    [SerializeField] AssetFolder _assetFolder;

    void Start() {
        var path = _assetFolder.Path;
        Debug.Log(path);
    }
}
```

</details>

Asset-based references, primarily for Editor use.

---

<details><summary><b>RuntimeAsset</b></summary>

![RuntimeAsset](.github/RuntimeAsset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RuntimeAssetExample : MonoBehaviour {
    [SerializeField] RuntimeAsset _runtimeAsset;

    void Start() {
        var path = _runtimeAsset.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>Runtime<></b></summary>

![GenericRuntimeAsset](.github/GenericRuntimeAsset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class GenericRuntimeAssetExample : MonoBehaviour {
    [SerializeField] Runtime<Material> _runtimeAsset;

    void Start() {
        var path = _runtimeAsset.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>RuntimeFile</b></summary>

![RuntimeFile](.github/RuntimeFile.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RuntimeFileExample : MonoBehaviour {
    [SerializeField] RuntimeFile _runtimeFile;

    void Start() {
        var path = _runtimeFile.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>RuntimeFolder</b></summary>

![RuntimeFolder](.github/RuntimeFolder.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RuntimeFolderExample : MonoBehaviour {
    [SerializeField] RuntimeFolder _runtimeFolder;

    void Start() {
        var path = _runtimeFolder.Path;
        Debug.Log(path);
    }
}
```

</details>

A type to hold any runtime assets: Resources, StreamingAssets or scenes.

---

<details><summary><b>Resource</b></summary>

![Resource](.github/Resource.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class ResourceExample : MonoBehaviour {
    [SerializeField] Resource _resource;

    void Start() {
        var path = _resource.Path;
        Debug.Log(path);
    }
}
```

</details>

<details><summary><b>Resource<></b></summary>

![GenericResource](.github/GenericResource.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class GenericResourceExample : MonoBehaviour {
    [SerializeField] Resource<Material> _resource;

    void Start() {
        var path = _resource.Path;
        Debug.Log(path);

        var material = _resource.Load(); 
        // var material = _resource.Load(typeof(Material)); 
        // var material = _resource.Load<Material>();
    }
}
```

```csharp
using System.Collections;
using ReferenceAnyPath;
using UnityEngine;

public class GenericResourceExample : MonoBehaviour {
    [SerializeField] Resource<Material> _resource;

    IEnumerator Start() {
        var path = _resource.Path;
        Debug.Log(path);

        var request = _resource.LoadAsync();
        // var request = _resource.LoadAsync(typeof(Material));
        // var request = _resource.LoadAsync<Material>();
        yield return request;
        var material = request.asset as Material;
    }
}
```

</details>

<details><summary><b>ResourceFile</b></summary>

![ResourceFile](.github/ResourceFile.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class ResourceFileExample : MonoBehaviour {
    [SerializeField] ResourceFile _resourceFile;

    void Start() {
        var path = _resourceFile.Path;
        Debug.Log(path);

        var material = _resourceFile.Load() as Material;
        // var material = _resourceFile.Load(typeof(Material)) as Material;
        // var material = _resourceFile.Load<Material>();
    }
}
```

```csharp
using System.Collections;
using ReferenceAnyPath;
using UnityEngine;

public class ResourceFileExample : MonoBehaviour {
    [SerializeField] ResourceFile _resourceFile;

    IEnumerator Start() {
        var path = _resourceFile.Path;
        Debug.Log(path);

        var request = _resourceFile.LoadAsync();
        // var request = _resourceFile.LoadAsync(typeof(Material));
        // var request = _resourceFile.LoadAsync<Material>();
        yield return request;
        var material = request.asset as Material;
    }
}
```

</details>

<details><summary><b>ResourceFolder</b></summary>

![ResourceFolder](.github/ResourceFolder.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class ResourceFolderExample : MonoBehaviour {
    [SerializeField] ResourceFolder _resourceFolder;

    void Start() {
        var path = _resourceFolder.Path;
        Debug.Log(path);

        Object[] materials = _resourceFolder.LoadAll();
        // Object[] materials = _resourceFolder.LoadAll(typeof(Material));
        // Material[] materials = _resourceFolder.LoadAll<Material>();
    }
}
```

</details>

<details><summary><b>TextResource</b></summary>

![TextResource](.github/TextResource.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class TextResourceExample : MonoBehaviour {
    [SerializeField] TextResource _textResource;

    void Start() {
        var path = _textResource.Path;
        Debug.Log(path);

        var text = _textResource.Load();
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class TextResourceExample : MonoBehaviour {
    [SerializeField] TextResource _textResource;

    async void Start() {
        var path = _textResource.Path;
        Debug.Log(path);

        try {
            var text = await _textResource.LoadAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class TextResourceExample : MonoBehaviour {
    [SerializeField] TextResource _textResource;

    async void Start() {
        var path = _textResource.Path;
        Debug.Log(path);

        try {
            var bytes = await _textResource.LoadAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

</details>

<details><summary><b>BinaryResource</b></summary>

![BinaryResource](.github/BinaryResource.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class BinaryResourceExample : MonoBehaviour {
    [SerializeField] BinaryResource _binaryResource;

    void Start() {
        var path = _binaryResource.Path;
        Debug.Log(path);

        var bytes = _binaryResource.Load();
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class BinaryResourceExample : MonoBehaviour {
    [SerializeField] BinaryResource _binaryResource;

    async void Start() {
        var path = _binaryResource.Path;
        Debug.Log(path);

        try {
            var bytes = await _binaryResource.LoadAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class BinaryResourceExample : MonoBehaviour {
    [SerializeField] BinaryResource _binaryResource;

    async void Start() {
        var path = _binaryResource.Path;
        Debug.Log(path);

        try {
            var bytes = await _binaryResource.LoadAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

</details>

Reference any Resources. A path is exposed for runtime, plus there are  loading methods available that replicated the methods of Resources class .

---

<details><summary><b>StreamingAsset</b></summary>

![StreamingAsset](.github/StreamingAsset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class StreamingAssetExample : MonoBehaviour {
    [SerializeField] StreamingAsset _streamingAsset;

    void Start() {
        var path = _streamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _streamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);
    }
}
```

</details>

<details><summary><b>StreamingAssetFile</b></summary>

![StreamingAssetFile](.github/StreamingAssetFile.png)

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class StreamingAssetFileExample : MonoBehaviour {
    [SerializeField] StreamingAssetFile _streamingAssetFile;

    async void Start() {
        var path = _streamingAssetFile.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _streamingAssetFile.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        try {
            var text = await _streamingAssetFile.ReadAllTextAsync();
            // var bytes = await _streamingAssetFile.ReadAllBytesAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class StreamingAssetFileExample : MonoBehaviour {
    [SerializeField] StreamingAssetFile _streamingAssetFile;

    async void Start() {
        var path = _streamingAssetFile.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _streamingAssetFile.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        try {
            var text = await _streamingAssetFile.ReadAllTextAsync(destroyCancellationToken);
            // var bytes = await _streamingAssetFile.ReadAllBytesAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}

```

</details>

<details><summary><b>StreamingAssetFolder</b></summary>

![StreamingAssetFolder](.github/StreamingAssetFolder.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class StreamingAssetFolderExample : MonoBehaviour {
    [SerializeField] StreamingAssetFolder _streamingAssetFolder;

    void Start() {
        var path = _streamingAssetFolder.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _streamingAssetFolder.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);
    }
}
```

</details>

<details><summary><b>TextStreamingAsset</b></summary>

![TextStreamingAsset](.github/TextStreamingAsset.png)

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class TextStreamingAssetExample : MonoBehaviour {
    [SerializeField] TextStreamingAsset _textStreamingAsset;

    async void Start() {
        var path = _textStreamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _textStreamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        try {
            var text = await _textStreamingAsset.ReadAllTextAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class TextStreamingAssetExample : MonoBehaviour {
    [SerializeField] TextStreamingAsset _textStreamingAsset;

    async void Start() {
        var path = _textStreamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _textStreamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        try {
            var text = await _textStreamingAsset.ReadAllTextAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

</details>

<details><summary><b>BinaryStreamingAsset</b></summary>

![BinaryStreamingAsset](.github/BinaryStreamingAsset.png)

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class BinaryStreamingAssetExample : MonoBehaviour {
    [SerializeField] BinaryStreamingAsset _binaryStreamingAsset;

    async void Start() {
        var path = _binaryStreamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _binaryStreamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        try {
            var bytes = await _binaryStreamingAsset.ReadAllBytesAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class BinaryStreamingAssetExample : MonoBehaviour {
    [SerializeField] BinaryStreamingAsset _binaryStreamingAsset;

    async void Start() {
        var path = _binaryStreamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _binaryStreamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        try {
            var bytes = await _binaryStreamingAsset.ReadAllBytesAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

</details>

Allows you to reference assets in the StreamingAssets folder.

---

<details><summary><b>Scene</b></summary>

![Scene](.github/Scene.png)

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = ReferenceAnyPath.Scene;

public class SceneExample : MonoBehaviour {
    [SerializeField] Scene _scene;

    void Start() {
        var path = _scene.Path;
        Debug.Log(path);

        var sceneName = _scene.Name;
        Debug.Log(sceneName);

        _scene.LoadScene();
        // _scene.LoadScene(LoadSceneMode.Additive);
        // _scene.LoadScene(new LoadSceneParameters {
        //     loadSceneMode = LoadSceneMode.Single,
        //     localPhysicsMode = LocalPhysicsMode.Physics2D
        // });
    }
}
```

```csharp
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = ReferenceAnyPath.Scene;

public class SceneExample : MonoBehaviour {
    [SerializeField] Scene _scene;

    IEnumerator Start() {
        var path = _scene.Path;
        Debug.Log(path);

        var sceneName = _scene.Name;
        Debug.Log(sceneName);

        yield return _scene.LoadSceneAsync();
        // yield return _scene.LoadSceneAsync(LoadSceneMode.Additive);
        // yield return _scene.LoadSceneAsync(new LoadSceneParameters {
        //     loadSceneMode = LoadSceneMode.Single,
        //     localPhysicsMode = LocalPhysicsMode.Physics2D
        // });
    }
}
```

</details>

A simple implementation of scene references. Exposes scene path and scene name for use in runtime.

---

<details><summary><b>RawHeightmapFile</b></summary>

![RawHeightmapFile](.github/RawHeightmapFile.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapFileExample : MonoBehaviour {
    [SerializeField] RawHeightmapFile _rawHeightmapFile;

    void Start() {
        var path = _rawHeightmapFile.Path;
        Debug.Log(path);

        var width = _rawHeightmapFile.Width;
        var height = _rawHeightmapFile.Height;
        var bits = _rawHeightmapFile.Bits;
        var byteOrder = _rawHeightmapFile.ByteOrder;
        var flip = _rawHeightmapFile.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");
    }
}
```

</details>


<details><summary><b>RawHeightmapAsset</b></summary>

![RawHeightmapAsset](.github/RawHeightmapAsset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapAssetExample : MonoBehaviour {
    [SerializeField] RawHeightmapAsset _rawHeightmapAsset;

    void Start() {
        var path = _rawHeightmapAsset.Path;
        Debug.Log(path);

        var width = _rawHeightmapAsset.Width;
        var height = _rawHeightmapAsset.Height;
        var bits = _rawHeightmapAsset.Bits;
        var byteOrder = _rawHeightmapAsset.ByteOrder;
        var flip = _rawHeightmapAsset.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");
    }
}
```

</details>


<details><summary><b>RawHeightmapRuntimeAsset</b></summary>

![RawHeightmapRuntimeAsset](.github/RawHeightmapRuntimeAsset.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapRuntimeAssetExample : MonoBehaviour {
    [SerializeField] RawHeightmapRuntimeAsset _rawHeightmapRuntimeAsset;

    void Start() {
        var path = _rawHeightmapRuntimeAsset.Path;
        Debug.Log(path);

        var width = _rawHeightmapRuntimeAsset.Width;
        var height = _rawHeightmapRuntimeAsset.Height;
        var bits = _rawHeightmapRuntimeAsset.Bits;
        var byteOrder = _rawHeightmapRuntimeAsset.ByteOrder;
        var flip = _rawHeightmapRuntimeAsset.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");
    }
}
```

</details>


<details><summary><b>RawHeightmapResource</b></summary>

![RawHeightmapResource](.github/RawHeightmapResource.png)

```csharp
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapResourceExample : MonoBehaviour {
    [SerializeField] RawHeightmapResource _rawHeightmapResource;

    void Start() {
        var path = _rawHeightmapResource.Path;
        Debug.Log(path);

        var width = _rawHeightmapResource.Width;
        var height = _rawHeightmapResource.Height;
        var bits = _rawHeightmapResource.Bits;
        var byteOrder = _rawHeightmapResource.ByteOrder;
        var flip = _rawHeightmapResource.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");

        var bytes = _rawHeightmapResource.Load();
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapResourceExample : MonoBehaviour {
    [SerializeField] RawHeightmapResource _rawHeightmapResource;

    async void Start() {
        var path = _rawHeightmapResource.Path;
        Debug.Log(path);

        var width = _rawHeightmapResource.Width;
        var height = _rawHeightmapResource.Height;
        var bits = _rawHeightmapResource.Bits;
        var byteOrder = _rawHeightmapResource.ByteOrder;
        var flip = _rawHeightmapResource.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");

        try {
            var bytes = await _rawHeightmapResource.LoadAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapResourceExample : MonoBehaviour {
    [SerializeField] RawHeightmapResource _rawHeightmapResource;

    async void Start() {
        var path = _rawHeightmapResource.Path;
        Debug.Log(path);

        var width = _rawHeightmapResource.Width;
        var height = _rawHeightmapResource.Height;
        var bits = _rawHeightmapResource.Bits;
        var byteOrder = _rawHeightmapResource.ByteOrder;
        var flip = _rawHeightmapResource.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");

        try {
            var bytes = await _rawHeightmapResource.LoadAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

</details>

<details><summary><b>RawHeightmapStreamingAsset</b></summary>

![RawHeightmapStreamingAsset](.github/RawHeightmapStreamingAsset.png)

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapStreamingAssetExample : MonoBehaviour {
    [SerializeField] RawHeightmapStreamingAsset _rawHeightmapStreamingAsset;

    async void Start() {
        var path = _rawHeightmapStreamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _rawHeightmapStreamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        var width = _rawHeightmapStreamingAsset.Width;
        var height = _rawHeightmapStreamingAsset.Height;
        var bits = _rawHeightmapStreamingAsset.Bits;
        var byteOrder = _rawHeightmapStreamingAsset.ByteOrder;
        var flip = _rawHeightmapStreamingAsset.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");

        try {
            var bytes = await _rawHeightmapStreamingAsset.ReadAllBytesAsync();
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```

```csharp
using System;
using ReferenceAnyPath;
using UnityEngine;

public class RawHeightmapStreamingAssetExample : MonoBehaviour {
    [SerializeField] RawHeightmapStreamingAsset _rawHeightmapStreamingAsset;

    async void Start() {
        var path = _rawHeightmapStreamingAsset.Path; // Path within StreamingAssets folder
        Debug.Log(path);

        var streamingAssetPath = _rawHeightmapStreamingAsset.StreamingAssetPath; // Path with StreamingAssets folder
        Debug.Log(streamingAssetPath);

        var width = _rawHeightmapStreamingAsset.Width;
        var height = _rawHeightmapStreamingAsset.Height;
        var bits = _rawHeightmapStreamingAsset.Bits;
        var byteOrder = _rawHeightmapStreamingAsset.ByteOrder;
        var flip = _rawHeightmapStreamingAsset.Flip;
        Debug.Log($"w: {width}, h: {height}, bits: {bits}, byte order: {byteOrder}, flip: {flip}");

        try {
            var bytes = await _rawHeightmapStreamingAsset.ReadAllBytesAsync(destroyCancellationToken);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
```
</details>

Unity terrains utilize raw heightmaps but Unity doesn't have a proper type for them. This is an attempt to create one. Besides referencing the file itself, it also stores some additional data required for a heightmap to be properly parsed: Width, Height, Bits, ByteOrder and Flip properties. Note that these types only store the data, they don't do any parsing.

---

### Extensions

You can restrict file types even further by adding a list of extensions. `,` and ` ` can be used as list separators. This is not supported for folders, and not supported for generics (as restriction is already applied via generic parameter).

---

### Editor and Unsafe methods

#### Runtime

- `Path` - runtime path for use in runtime/builds
- `PathUnsafe` - unsafe version for convenience

Unsafe path is the "packed" version of the path that Unity actually serializes. The reason for this is that unity `SerializedProperty` doesn't distinguish between `null` and empty strings and stores them as empty. But empty string is a valid path for a folder in various cases (e.g., Resources or Asset folder). So to overcome this limitation, this info is "packed" into the serialized path:

| PathUnsafe | Path      |
|------------|-----------|
| ""         | null      | 
| "."        | ""        |
| some path  | some path |

`Path` unpacks the data, `PathUnsafe` enables access to the original value.

#### Editor-only

- `AbsolutePath`
- `RelativePath`
- `AssetPath`
- `RuntimePath`

Each of them not only unpacks the data just like `Path` does, but also checks the path for existence. `AbsolutePath` and `RelativePath` access the disc and `AssetPath` and `RuntimePath` access the `AssetDatabase`. Avoid using these methods in the main Editor/application loop, instead consider using unsafe alternatives:

- `AbsolutePathUnsafe`
- `RelativePathUnsafe`
- `AssetPathUnsafe`
- `RuntimePathUnsafe`

Unsafe methods simply return what is stored in a property; packing is the same as for `Path`.

---

### Interfaces

<details><summary>Runtime types feature a comprehensive set of interfaces:</summary>

```csharp
#if UNITY_EDITOR
    public interface IEditorPaths {
        public string RelativePath { get; }
        public string AbsolutePath { get; }
        public string AssetPath { get; }
        public string RuntimePath { get; }
    }

    public interface IEditorPathsUnsafe : IEditorPaths {
        public string RelativePathUnsafe { get; }
        public string AbsolutePathUnsafe { get; }
        public string AssetPathUnsafe { get; }
        public string RuntimePathUnsafe { get; }
    }
#endif

#if UNITY_EDITOR
    public interface IPath : IEditorPaths {
#else
    public interface IPath {
#endif
        public string Path { get; }
    }

#if UNITY_EDITOR
    public interface IPathUnsafe : IPath, IEditorPathsUnsafe {
#else
    public interface IPathUnsafe : IPath {
#endif
        public string PathUnsafe { get; }
    }

    public interface IAnyPath : IPathUnsafe { }
    public interface IAnyFile : IAnyPath { }
    public interface IAnyFolder : IAnyPath { }

    public interface IAsset : IAnyPath { }
    public interface IAssetFile : IAsset, IAnyFile { }
    public interface IAssetFolder : IAsset, IAnyFolder { }

    public interface IRuntimeAsset : IAsset { }
    public interface IRuntimeFile : IRuntimeAsset, IAssetFile { }
    public interface IRuntimeFolder : IRuntimeAsset, IAssetFolder { }

    public interface IResource : IRuntimeAsset { }

    public interface IResourceFile : IResource, IRuntimeFile { }

    public interface ITextResourceFile : IResourceFile {
        public string Load();

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> LoadAsync();
        public UniTask<string> LoadAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> LoadAsync();
        public Awaitable<string> LoadAsync(CancellationToken cancellationToken);
#else
        public Task<string> LoadAsync();
        public Task<string> LoadAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IBinaryResourceFile : IResourceFile {
        public byte[] Load();

#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<byte[]> LoadAsync();
        public UniTask<byte[]> LoadAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<byte[]> LoadAsync();
        public Awaitable<byte[]> LoadAsync(CancellationToken cancellationToken);
#else
        public Task<byte[]> LoadAsync();
        public Task<byte[]> LoadAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IResourceFile<TObject> : IResourceFile {
        public TObject Load();
        public T Load<T>() where T : TObject;
        public TObject Load(Type systemTypeInstance);
        public ResourceRequest LoadAsync();
        public ResourceRequest LoadAsync<T>() where T : TObject;
        public ResourceRequest LoadAsync(Type systemTypeInstance);
    }

    public interface IResourceFolder : IResource, IRuntimeFolder {
        public UnityObject[] LoadAll();
        public T[] LoadAll<T>() where T : UnityObject;
        public UnityObject[] LoadAll(Type systemTypeInstance);
    }

    public interface IStreamingAsset : IRuntimeAsset {
        public string StreamingAssetPath { get; }
    }

    public interface IStreamingAssetFile : IStreamingAsset, IRuntimeFile { }

    public interface ITextStreamingAssetFile : IStreamingAssetFile {
#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<string> ReadAllTextAsync();
        public UniTask<string> ReadAllTextAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<string> ReadAllTextAsync();
        public Awaitable<string> ReadAllTextAsync(CancellationToken cancellationToken);
#else
        public Task<string> ReadAllTextAsync();
        public Task<string> ReadAllTextAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IBinaryStreamingAssetFile : IStreamingAssetFile {
#if REFERENCE_ANY_PATH_FORCE_UNITASK
        public UniTask<byte[]> ReadAllBytesAsync();
        public UniTask<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken);
#elif UNITY_2023_1_OR_NEWER
        public Awaitable<byte[]> ReadAllBytesAsync();
        public Awaitable<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken);
#else
        public Task<byte[]> ReadAllBytesAsync();
        public Task<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken);
#endif
    }

    public interface IStreamingAssetFolder : IStreamingAsset, IRuntimeFolder { }

    public interface IScene : IRuntimeFile {
        public string Name { get; }
        public void LoadScene();
        public void LoadScene(LoadSceneMode mode);
        public void LoadScene(LoadSceneParameters parameters);
        public AsyncOperation LoadSceneAsync();
        public AsyncOperation LoadSceneAsync(LoadSceneMode mode);
        public AsyncOperation LoadSceneAsync(LoadSceneParameters parameters);
    }

    public interface IRawHeightmap : IAnyFile {
        public int Width { get;}
        public int Height { get;}
        public int Bits { get;}
        public ByteOrder ByteOrder { get; }
        public Flip Flip { get; }
    }

    public interface IRawHeightmapFile : IRawHeightmap { }
    public interface IRawHeightmapAsset : IRawHeightmap, IAssetFile { }
    public interface IRawHeightmapRuntimeAsset : IRawHeightmap, IRuntimeFile { }
    public interface IRawHeightmapResource : IRawHeightmap, IBinaryResourceFile { }
    public interface IRawHeightmapStreamingAsset : IRawHeightmap, IBinaryStreamingAssetFile { }
```
</details>

---

### Scripting defines

- REFERENCE_ANY_PATH_NO_VALIDATION_BEFORE_SERIALIZATION — removes path check and restoration code from `OnBeforeSerialize`.

- REFERENCE_ANY_PATH_NO_PARALLEL_CHECK_IN_INSPECTOR — removes non-asset paths validation from `PropertyDrawer`.

- REFERENCE_ANY_PATH_FORCE_UNITASK — forces the use of UniTask for async/parallel operations. Only enable it if you have UniTask in your project. Also, I would probably suggest not using this at all, unless you have performance issues or some other issues with the tasks.
