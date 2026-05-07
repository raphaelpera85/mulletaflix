using System;
using System.Collections.Generic;
using MediaBrowser.Model.Configuration;

namespace MediaBrowser.Controller.MediaEncoding
{
    /// <summary>
    /// Interface for managing and querying FFmpeg/FFprobe capabilities.
    /// </summary>
    public interface IFFmpegCapabilityManager
    {
        /// <summary>
        /// Gets the FFmpeg version.
        /// </summary>
        Version? FFmpegVersion { get; }

        /// <summary>
        /// Gets a value indicating whether p key pausing is supported.
        /// </summary>
        bool IsPkeyPauseSupported { get; }

        /// <summary>
        /// Gets a value indicating whether the configured Vaapi device is from AMD.
        /// </summary>
        bool IsVaapiDeviceAmd { get; }

        /// <summary>
        /// Gets a value indicating whether the configured Vaapi device is from Intel (iHD).
        /// </summary>
        bool IsVaapiDeviceInteliHD { get; }

        /// <summary>
        /// Gets a value indicating whether the configured Vaapi device is from Intel (i965).
        /// </summary>
        bool IsVaapiDeviceInteli965 { get; }

        /// <summary>
        /// Gets a value indicating whether the configured Vaapi device supports vulkan drm format modifier.
        /// </summary>
        bool IsVaapiDeviceSupportVulkanDrmModifier { get; }

        /// <summary>
        /// Gets a value indicating whether the configured Vaapi device supports vulkan drm interop.
        /// </summary>
        bool IsVaapiDeviceSupportVulkanDrmInterop { get; }

        /// <summary>
        /// Gets a value indicating whether av1 decoding is available via VideoToolbox.
        /// </summary>
        bool IsVideoToolboxAv1DecodeAvailable { get; }

        /// <summary>
        /// Gets a value indicating whether the prober supports the only_first_vframe option.
        /// </summary>
        bool ProberSupportsFirstVideoFrame { get; }

        /// <summary>
        /// Gets a value indicating whether low priority hardware decoding is supported.
        /// </summary>
        bool IsLowPriorityHwDecodeSupported { get; }

        /// <summary>
        /// Whether the encoder codec is supported.
        /// </summary>
        /// <param name="encoder">The encoder to check.</param>
        /// <returns>A value indicating whether the encoder is supported.</returns>
        bool SupportsEncoder(string encoder);

        /// <summary>
        /// Whether the decoder codec is supported.
        /// </summary>
        /// <param name="decoder">The decoder to check.</param>
        /// <returns>A value indicating whether the decoder is supported.</returns>
        bool SupportsDecoder(string decoder);

        /// <summary>
        /// Whether the hardware acceleration type is supported.
        /// </summary>
        /// <param name="hwaccel">The hardware acceleration type to check.</param>
        /// <returns>A value indicating whether the hardware acceleration type is supported.</returns>
        bool SupportsHwaccel(string hwaccel);

        /// <summary>
        /// Whether the filter is supported.
        /// </summary>
        /// <param name="filter">The filter to check.</param>
        /// <returns>A value indicating whether the filter is supported.</returns>
        bool SupportsFilter(string filter);

        /// <summary>
        /// Whether the filter is supported with the given option.
        /// </summary>
        /// <param name="option">The filter option type to check.</param>
        /// <returns>A value indicating whether the filter is supported with the given option.</returns>
        bool SupportsFilterWithOption(FilterOptionType option);

        /// <summary>
        /// Whether the bitstream filter is supported with the given option.
        /// </summary>
        /// <param name="option">The bitstream filter option type to check.</param>
        /// <returns>A value indicating whether the bitstream filter is supported with the given option.</returns>
        bool SupportsBitStreamFilterWithOption(BitStreamFilterOptionType option);

        /// <summary>
        /// Initializes the capabilities by probing the specified FFmpeg/FFprobe binaries.
        /// </summary>
        /// <param name="ffmpegPath">The path to the FFmpeg binary.</param>
        /// <param name="ffprobePath">The path to the FFprobe binary.</param>
        /// <param name="options">The encoding options.</param>
        void Initialize(string ffmpegPath, string ffprobePath, EncodingOptions options);
    }
}
