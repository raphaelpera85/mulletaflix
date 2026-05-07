using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.MediaEncoding;
using MediaBrowser.Model.Configuration;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Logging;

namespace MediaBrowser.MediaEncoding.Encoder
{
    /// <summary>
    /// Implementation of IFFmpegCapabilityManager.
    /// </summary>
    public class FFmpegCapabilityManager : IFFmpegCapabilityManager
    {
        private readonly ILogger<FFmpegCapabilityManager> _logger;
        private readonly ILogger<EncoderValidator> _validatorLogger;

        private static readonly string[] _vulkanImageDrmFmtModifierExts = { "VK_EXT_image_drm_format_modifier" };

        private static readonly string[] _vulkanExternalMemoryDmaBufExts =
        {
            "VK_KHR_external_memory_fd",
            "VK_EXT_external_memory_dma_buf",
            "VK_KHR_external_semaphore_fd",
            "VK_EXT_external_memory_host"
        };

        private Version? _ffmpegVersion;

        private List<string> _encoders = new();

        private List<string> _decoders = new();

        private List<string> _hwaccels = new();

        private List<string> _filters = new();

        private IDictionary<FilterOptionType, bool> _filtersWithOption = new Dictionary<FilterOptionType, bool>();

        private IDictionary<BitStreamFilterOptionType, bool> _bitStreamFiltersWithOption = new Dictionary<BitStreamFilterOptionType, bool>();

        private bool _isPkeyPauseSupported;

        private bool _isVaapiDeviceAmd;

        private bool _isVaapiDeviceInteliHD;

        private bool _isVaapiDeviceInteli965;

        private bool _isVaapiDeviceSupportVulkanDrmModifier;

        private bool _isVaapiDeviceSupportVulkanDrmInterop;

        private bool _isVideoToolboxAv1DecodeAvailable;

        private bool _proberSupportsFirstVideoFrame;

        private bool _isLowPriorityHwDecodeSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="FFmpegCapabilityManager"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="validatorLogger">The validator logger.</param>
        public FFmpegCapabilityManager(
            ILogger<FFmpegCapabilityManager> logger,
            ILogger<EncoderValidator> validatorLogger)
        {
            _logger = logger;
            _validatorLogger = validatorLogger;
        }

        /// <inheritdoc />
        public Version? FFmpegVersion => _ffmpegVersion;

        /// <inheritdoc />
        public bool IsPkeyPauseSupported => _isPkeyPauseSupported;

        /// <inheritdoc />
        public bool IsVaapiDeviceAmd => _isVaapiDeviceAmd;

        /// <inheritdoc />
        public bool IsVaapiDeviceInteliHD => _isVaapiDeviceInteliHD;

        /// <inheritdoc />
        public bool IsVaapiDeviceInteli965 => _isVaapiDeviceInteli965;

        /// <inheritdoc />
        public bool IsVaapiDeviceSupportVulkanDrmModifier => _isVaapiDeviceSupportVulkanDrmModifier;

        /// <inheritdoc />
        public bool IsVaapiDeviceSupportVulkanDrmInterop => _isVaapiDeviceSupportVulkanDrmInterop;

        /// <inheritdoc />
        public bool IsVideoToolboxAv1DecodeAvailable => _isVideoToolboxAv1DecodeAvailable;

        /// <inheritdoc />
        public bool ProberSupportsFirstVideoFrame => _proberSupportsFirstVideoFrame;

        /// <inheritdoc />
        public bool IsLowPriorityHwDecodeSupported => _isLowPriorityHwDecodeSupported;

        /// <inheritdoc />
        public bool SupportsEncoder(string encoder) => _encoders.Contains(encoder, StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc />
        public bool SupportsDecoder(string decoder) => _decoders.Contains(decoder, StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc />
        public bool SupportsHwaccel(string hwaccel) => _hwaccels.Contains(hwaccel, StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc />
        public bool SupportsFilter(string filter) => _filters.Contains(filter, StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc />
        public bool SupportsFilterWithOption(FilterOptionType option) => _filtersWithOption.TryGetValue(option, out var val) && val;

        /// <inheritdoc />
        public bool SupportsBitStreamFilterWithOption(BitStreamFilterOptionType option) => _bitStreamFiltersWithOption.TryGetValue(option, out var val) && val;

        /// <inheritdoc />
        public void Initialize(string ffmpegPath, string ffprobePath, EncodingOptions options)
        {
            if (string.IsNullOrEmpty(ffmpegPath))
            {
                return;
            }

            var validator = new EncoderValidator(_validatorLogger, ffmpegPath);

            _decoders = validator.GetDecoders().ToList();
            _encoders = validator.GetEncoders().ToList();
            _filters = validator.GetFilters().ToList();
            _filtersWithOption = validator.GetFiltersWithOption();
            _bitStreamFiltersWithOption = validator.GetBitStreamFiltersWithOption();
            _hwaccels = validator.GetHwaccels().ToList();
            _ffmpegVersion = validator.GetFFmpegVersion()!;

            _isPkeyPauseSupported = validator.CheckSupportedRuntimeKey("p      pause transcoding", _ffmpegVersion);
            _isLowPriorityHwDecodeSupported = validator.CheckSupportedHwaccelFlag("low_priority");
            _proberSupportsFirstVideoFrame = validator.CheckSupportedProberOption("only_first_vframe", ffprobePath);

            // Check the Vaapi device vendor
            if (OperatingSystem.IsLinux()
                && SupportsHwaccel("vaapi")
                && !string.IsNullOrEmpty(options.VaapiDevice)
                && options.HardwareAccelerationType == HardwareAccelerationType.vaapi)
            {
                _isVaapiDeviceAmd = validator.CheckVaapiDeviceByDriverName("Mesa Gallium driver", options.VaapiDevice);
                _isVaapiDeviceInteliHD = validator.CheckVaapiDeviceByDriverName("Intel iHD driver", options.VaapiDevice);
                _isVaapiDeviceInteli965 = validator.CheckVaapiDeviceByDriverName("Intel i965 driver", options.VaapiDevice);
                _isVaapiDeviceSupportVulkanDrmModifier = validator.CheckVulkanDrmDeviceByExtensionName(options.VaapiDevice, _vulkanImageDrmFmtModifierExts);
                _isVaapiDeviceSupportVulkanDrmInterop = validator.CheckVulkanDrmDeviceByExtensionName(options.VaapiDevice, _vulkanExternalMemoryDmaBufExts);
            }

            // Check if VideoToolbox supports AV1 decode
            if (OperatingSystem.IsMacOS() && SupportsHwaccel("videotoolbox"))
            {
                _isVideoToolboxAv1DecodeAvailable = validator.CheckIsVideoToolboxAv1DecodeAvailable();
            }
        }
    }
}
