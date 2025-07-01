using SimpleFileBrowser;
using Tayx.Graphy.Advanced;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInfo : MonoBehaviour
{
    [Header("Obj Main")]
    public App_Handle app;

    [Header("UI Ram")]
    public Text txtRamReserved;
    public Text txtRamAllocated;
    public Text txtRamMono;

    [Header("UI FPS")]
    public Text TxtCurrentFps;
    public Text TxtMinFps;
    public Text TextMaxFps;
    public Text TextAvgFps;
    public Text txtFpsTimer;

    [Header("UI Audio")]
    public Text txtAudioDb;
    [Header("UI Os")]
    public G_AdvancedData g_Advanced;

    public void SaveInfo()
    {
        app.file.Set_filter(Carrot.Carrot_File_Data.TextDocument);
        app.file.Save_file(paths =>
        {
            string textContent = "GPU Master Monitor\n";
            if (app.is_info_ram)
            {
                textContent += "----------Ram-----------\n";
                textContent += "Reserved:\t" + txtRamReserved.text + " MB\n";
                textContent += "Allocated:\t" + txtRamAllocated.text + " MB\n";
                textContent += "Mono:\t\t" + txtRamMono.text + " MB\n";
            }
            textContent += "----------FPS-----------\n";
            textContent += "Current Fps:\t" + TxtCurrentFps.text + " FPS\n";
            textContent += "Min Fps:\t" + TxtMinFps.text + " FPS\n";
            textContent += "Max Fps:\t" + TextMaxFps.text + " FPS\n";
            textContent += "Timer Fps:\t" + txtFpsTimer.text + " MS\n";
            textContent += "----------Audio---------\n";
            textContent += "Sound intensity:\t" + txtAudioDb.text + " dB\n";
            if (app.is_info_advanced)
            {
                textContent += "----------OS------------\n";
                textContent += g_Advanced.m_graphicsDeviceVersionText.text + "\n";
                textContent += g_Advanced.m_processorTypeText.text + "\n";
                textContent += g_Advanced.m_operatingSystemText.text + "\n";
                textContent += g_Advanced.m_systemMemoryText.text + "\n";
                textContent += g_Advanced.m_graphicsDeviceNameText.text + "\n";
                textContent += g_Advanced.m_graphicsMemorySizeText.text + "\n";
                textContent += g_Advanced.m_screenResolutionText.text + "\n";
                textContent += g_Advanced.m_gameWindowResolutionText.text + "\n";
            }
            textContent += "------------------------\n";
            FileBrowserHelpers.WriteTextToFile(paths[0], textContent);
            app.carrot.Show_msg("Save Information", "Save file text at:\n" + paths[0], Carrot.Msg_Icon.Success);
        }, null, "GPU Master Monitor");
    }
}
