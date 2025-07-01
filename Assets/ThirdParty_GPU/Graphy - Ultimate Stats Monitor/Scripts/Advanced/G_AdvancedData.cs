/* ---------------------------------------
 * Author:          Martin Pane (martintayx@gmail.com) (@tayx94)
 * Collaborators:   Lars Aalbertsen (@Rockylars)
 * Project:         Graphy - Ultimate Stats Monitor
 * Date:            05-Dec-17
 * Studio:          Tayx
 * 
 * This project is released under the MIT license.
 * Attribution is not required, but it is always welcomed!
 * -------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using Tayx.Graphy.UI;
using Tayx.Graphy.Utils;
using Tayx.Graphy.Utils.NumString;

#if UNITY_5_5_OR_NEWER
using UnityEngine.Profiling;
#endif

namespace Tayx.Graphy.Advanced
{
    public class G_AdvancedData : MonoBehaviour, IMovable, IModifiableState
    {
        /* ----- TODO: ----------------------------
         * Add summaries to the variables.
         * Add summaries to the functions.
         * --------------------------------------*/

        #region Variables -> Serialized Private


        [SerializeField] public Text m_graphicsDeviceVersionText = null;

        [SerializeField] public Text m_processorTypeText = null;

        [SerializeField] public Text m_operatingSystemText = null;

        [SerializeField] public Text m_systemMemoryText = null;

        [SerializeField] public Text m_graphicsDeviceNameText = null;
        [SerializeField] public Text m_graphicsMemorySizeText = null;
        [SerializeField] public Text m_screenResolutionText = null;
        [SerializeField] public Text m_gameWindowResolutionText = null;

        [Range(1, 60)]
        [SerializeField] private float m_updateRate = 1f;  // 1 update per sec.

        #endregion

        #region Variables -> Private

        private GraphyManager m_graphyManager = null;

        private RectTransform m_rectTransform = null;

        private float m_deltaTime = 0.0f;

        private StringBuilder m_sb = null;

        private GraphyManager.ModuleState m_previousModuleState = GraphyManager.ModuleState.FULL;
        private GraphyManager.ModuleState m_currentModuleState = GraphyManager.ModuleState.FULL;

        private readonly string[] m_windowStrings =
        {
            "Window: ",
            "x",
            "@",
            "Hz",
            "[",
            "dpi]"
        };

        #endregion

        #region Methods -> Unity Callbacks

        private void OnEnable()
        {
            Init();
        }

        private void Update()
        {
            m_deltaTime += Time.unscaledDeltaTime;

            if (m_deltaTime > 1f / m_updateRate)
            {
                // Update screen window resolution
                m_sb.Length = 0;

                m_sb.Append(m_windowStrings[0]).Append(Screen.width.ToStringNonAlloc())
                    .Append(m_windowStrings[1]).Append(Screen.height.ToStringNonAlloc())
                    .Append(m_windowStrings[2]).Append(Screen.currentResolution.refreshRate.ToStringNonAlloc())
                    .Append(m_windowStrings[3])
                    .Append(m_windowStrings[4]).Append(Screen.dpi.ToStringNonAlloc())
                    .Append(m_windowStrings[5]);

                m_gameWindowResolutionText.text = m_sb.ToString();

                // Reset variables
                m_deltaTime = 0f;
            }
        }

        #endregion

        #region Methods -> Public

        public void SetPosition(GraphyManager.ModulePosition newModulePosition)
        {

            switch (newModulePosition)
            {
                case GraphyManager.ModulePosition.TOP_LEFT:
                case GraphyManager.ModulePosition.BOTTOM_LEFT:

                    m_processorTypeText.alignment = TextAnchor.UpperLeft;
                    m_systemMemoryText.alignment = TextAnchor.UpperLeft;
                    m_graphicsDeviceNameText.alignment = TextAnchor.UpperLeft;
                    m_graphicsDeviceVersionText.alignment = TextAnchor.UpperLeft;
                    m_graphicsMemorySizeText.alignment = TextAnchor.UpperLeft;
                    m_screenResolutionText.alignment = TextAnchor.UpperLeft;
                    m_gameWindowResolutionText.alignment = TextAnchor.UpperLeft;
                    m_operatingSystemText.alignment = TextAnchor.UpperLeft;

                    break;

                case GraphyManager.ModulePosition.TOP_RIGHT:
                case GraphyManager.ModulePosition.BOTTOM_RIGHT:

                    m_processorTypeText.alignment = TextAnchor.UpperRight;
                    m_systemMemoryText.alignment = TextAnchor.UpperRight;
                    m_graphicsDeviceNameText.alignment = TextAnchor.UpperRight;
                    m_graphicsDeviceVersionText.alignment = TextAnchor.UpperRight;
                    m_graphicsMemorySizeText.alignment = TextAnchor.UpperRight;
                    m_screenResolutionText.alignment = TextAnchor.UpperRight;
                    m_gameWindowResolutionText.alignment = TextAnchor.UpperRight;
                    m_operatingSystemText.alignment = TextAnchor.UpperRight;

                    break;

                case GraphyManager.ModulePosition.FREE:
                    break;
            }
        }

        public void SetState(GraphyManager.ModuleState state, bool silentUpdate = false)
        {
            if (!silentUpdate)
            {
                m_previousModuleState = m_currentModuleState;
            }

            m_currentModuleState = state;

            bool active = state == GraphyManager.ModuleState.FULL
                          || state == GraphyManager.ModuleState.TEXT
                          || state == GraphyManager.ModuleState.BASIC;

            gameObject.SetActive(active);
        }

        /// <summary>
        /// Restores state to the previous one.
        /// </summary>
        public void RestorePreviousState()
        {
            SetState(m_previousModuleState);
        }

        public void UpdateParameters()
        {

            SetPosition(m_graphyManager.AdvancedModulePosition);
            SetState(m_graphyManager.AdvancedModuleState);
        }

        public void RefreshParameters()
        {
            SetPosition(m_graphyManager.AdvancedModulePosition);
            SetState(m_currentModuleState, true);
        }

        #endregion

        #region Methods -> Private

        private void Init()
        {
            //TODO: Replace this with one activated from the core and figure out the min value.
            if (!G_FloatString.Inited
                || G_FloatString.MinValue > -1000f
                || G_FloatString.MaxValue < 16384f)
            {
                G_FloatString.Init
                (
                    minNegativeValue: -1001f,
                    maxPositiveValue: 16386f
                );
            }

            m_graphyManager = transform.root.GetComponentInChildren<GraphyManager>();

            m_sb = new StringBuilder();

            m_rectTransform = GetComponent<RectTransform>();

            #region Section -> Text

            m_processorTypeText.text
                = "CPU: "
                + SystemInfo.processorType
                + " ["
                + SystemInfo.processorCount
                + " cores]";

            m_systemMemoryText.text
                = "RAM: "
                + SystemInfo.systemMemorySize
                + " MB";

            m_graphicsDeviceVersionText.text
                = "Graphics API: "
                + SystemInfo.graphicsDeviceVersion;

            m_graphicsDeviceNameText.text
                = "GPU: "
                + SystemInfo.graphicsDeviceName;

            m_graphicsMemorySizeText.text
                = "VRAM: "
                + SystemInfo.graphicsMemorySize
                + "MB. Max texture size: "
                + SystemInfo.maxTextureSize
                + "px. Shader level: "
                + SystemInfo.graphicsShaderLevel;

            Resolution res = Screen.currentResolution;

            m_screenResolutionText.text
                = "Screen: "
                + res.width
                + "x"
                + res.height
                + "@"
                + res.refreshRate
                + "Hz";

            m_operatingSystemText.text
                = "OS: "
                + SystemInfo.operatingSystem
                + " ["
                + SystemInfo.deviceType
                + "]";

            float preferredWidth = 0;

            // Resize the background overlay

            List<Text> texts = new List<Text>()
            {
                m_graphicsDeviceVersionText,
                m_processorTypeText,
                m_systemMemoryText,
                m_graphicsDeviceNameText,
                m_graphicsMemorySizeText,
                m_screenResolutionText,
                m_gameWindowResolutionText,
                m_operatingSystemText
            };

            foreach (var text in texts)
            {
                if (text.preferredWidth > preferredWidth)
                {
                    preferredWidth = text.preferredWidth;
                }
            }

            #endregion



            UpdateParameters();
        }

        #endregion
    }
}