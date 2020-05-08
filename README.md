# URP-Equirectangular-Camera
Example Unity project showing 360° equirectangular camera for Universal Render Pipeline (URP). This can be used to convert Unity scenes to be viewed as 360 panoramas on platforms like YouTube.

The shader is adapted from [Mapiraz/CubemapToEquirectangular(https://github.com/Mapiarz/CubemapToEquirectangular) "Mapiraz/CubemapToEquirectangular"] which I could not get to work with URP initially.

![Example Panorama Image](https://github.com/s9lucas/URP-Equirectangular-Camera/blob/master/urp-equirectangular-camera-screenshot.png?raw=true)

## Getting Started

This repo contains an example project, tested in Unity 2019.3.12f1.

To include in your own project, copy /Assets/URPEquirectangularCamera into your project's /Assets. Add /Assets/URPEquirectangularCamera/CubemapCamera.prefab to your scene and disable other cameras.

You may need to activate Play mode to see the render texture activate but it should run and update in the editor after that.

## How does it work?

The primary components are:

  * Camera with CubemapRender.cs sets the camera to render to a CubeMap texture
  * CubeMapTexture.renderTexture that gets updated with CubeMap image from the camera
  * CubeMapMat.mat so that you can apply a shader
  * CubeMaptoEquirectangular.shader that remaps the texture image to equirectangular 360 format
  * EquirectangularCanvas that renders the texture with the shader as "Screen Space - Overlay" so that it replaces the camera on the display

You could potentially use a different .shader to accomplish another kind of output projection mapping and the process should be the same.

## Using the Camera Output

For my purposes, I use the following process:

  * set ProjectSettings/Player/Resolution to 3840x2160 Windowed so that the project will render at 4k after building
  * build the project for my computer (Windows)
  * capture the build's output with OBS to a fixed media video
  * trim, fade, etc. the video in your video editor of choice
  * pass the video through [Spatial Media Metadata Injector](https://github.com/google/spatial-media "Spatial Media Metadata Injector") to mark it as a 360 panorama
  * upload the video to a 360 panorama platform like [YouTube](https://www.youtube.com/ "YouTube")

## Adjusting Your Scene

You likely will have to tweak your scene to accomodate the cam, specifically things that may interact with the edges of the cubemap. I have found that VFX particle billboards pointed at the camera look better if they are smaller and further away from he camera. Post-processing that involves the camera frame like Vignette or extreme Bloom will interact with the cubemap edges. Depth of Field and other depth-based post-processing does not currently work because they are applied after the scene renders on the the canvas, which doesn't include the depth channel from the camera (there should be some way to fix this).

### What about higher resolutions?

You should be able to increase the player resolution to 8k or higher if your graphics card can support it. You should also increase the resolution of CubeMapTexture.renderTexture to accomodate this. Last time I tried an 8k panorama YouTube, it did not render smoothly so I preferred 4k.

## Bugs and Issues

Making changes on anything in the EquirectangularCanvas or start/stopping Play mode will often give this error:

```
Assertion failed on expression: 'texture->GetDimension() == kTexDim2D' UnityEngine.Canvas:SendWillRenderCanvases()
```

This doesn't seem to cause any operational problems and does not prevent building.

For some reason, the EquirectangularCanvas/Canvas interferes with lighting on the Scene view. Maybe there is a setting causing this. Disabling EquirectangularCanvas while in Scene mode is a workaround. Typically I would have already done all the scene design with a regular camera and then do tweaks specific to the CubeMap output. You could set up a script to make a hotkey enable/disable toggle between different cameras if needed.

