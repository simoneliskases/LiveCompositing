using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NativeLibrary
{
#if ENABLE_WEBCAM

    public static int GetVideoBitDepth()
    {
        return 0;
    }

    public static int GetVideoWidthResolution()
    {
        return 0;
    }

    public static int GetVideoHeightResolution()
    {
        return 0;
    }

    public static string GetVideoCodec()
    {
        return null;
    }

    public static string GetChromaSubsampling()
    {
        return null;
    }

#else

    public static int GetVideoBitDepth()
    {
        return 0;
    }

    public static int GetVideoWithResolution()
    {
        return 0;
    }

    public static int GetVideoHeightResolution()
    {
        return 0;
    }

    public static string GetVideoCodec()
    {
        return null;
    }

    public static string GetChromaSubsampling()
    {
        return null;
    }

#endif
}
