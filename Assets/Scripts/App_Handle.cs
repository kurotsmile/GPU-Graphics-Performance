using System.Collections;
using System.Collections.Generic;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.UI;

public class App_Handle : MonoBehaviour
{
    [Header("Obj Main")]
    public Carrot.Carrot carrot;
    public IronSourceAds ads;
    public GraphyManager g;
    public AudioSource sound_bk;
    private float timer_update_rank = 0;
    public float timer_max_update =10f;
    private bool is_update_rank = true;
    private bool is_info_advanced = true;
    private bool is_info_ram = true;

    [Header("Scene")]
    public Transform area_portrait;
    public Transform area_landspace;

    [Header("Info GPU")]
    public GameObject panel_fps;
    public GameObject panel_ram;
    public GameObject panel_fish;
    public GameObject panel_advanced;
    public GameObject panel_audio;

    [Header("Whale")]
    public Text txt_count_fish;
    public Transform arean_whale;
    public GameObject obj_whale_prefab;
    private List<GameObject> list_fish;

    [Header("Setting")]
    public Sprite sp_icon_cpu;
    public Sprite sp_icon_ram;
    public Sprite sp_icon_on;
    public Sprite sp_icon_off;
    private Carrot.Carrot_Box_Btn_Item setting_gpu_info_status;
    private Carrot.Carrot_Box_Btn_Item setting_ram_info_status;

    [Header("Effect")]
    public GameObject[] obj_effect_prefab;

    void Start()
    {
        this.carrot.Load_Carrot();
        this.carrot.act_after_close_all_box=this.enable_update_rank;
        this.list_fish = new List<GameObject>();
        this.ads.On_Load();
        this.carrot.delay_function(2f,()=>{
            this.carrot.game.load_bk_music(this.sound_bk);
            if (this.carrot.get_status_sound())
            {   
                this.panel_audio.SetActive(true);
                this.sound_bk.Play();
            }
            else
            {
                this.panel_audio.SetActive(false);
                this.sound_bk.Stop();
            }
        });

        this.add_whale();

        if (PlayerPrefs.GetInt("is_info_advanced", 0) == 0)
            this.is_info_advanced = true;
        else
            this.is_info_advanced = false;

        if (PlayerPrefs.GetInt("is_info_ram", 0) == 0)
            this.is_info_ram = true;
        else
            this.is_info_ram = false;

        this.check_rotate_scene();
    }

    private void Update()
    {
        if (this.is_update_rank)
        {
            this.timer_update_rank += 1f * Time.deltaTime;
            if (this.timer_update_rank > this.timer_max_update)
            {
                this.ads.ShowRewardedVideo();
                this.timer_update_rank = 0;
                this.update_rank();
            }
        }
    }

    private void enable_update_rank()
    {
        this.is_update_rank = true;
    }

    private void update_rank()
    {
        this.carrot.game.update_scores_player(this.list_fish.Count);
    }

    private void add_whale()
    {
       // this.carrot.ads.show_ads_Interstitial();
        GameObject obj_whale = Instantiate(this.obj_whale_prefab);
        obj_whale.transform.SetParent(this.arean_whale);
        obj_whale.transform.localPosition = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
        this.list_fish.Add(obj_whale);
        this.txt_count_fish.text = "x " + this.list_fish.Count;
    }

    public void btn_add_whale()
    {
        this.carrot.play_sound_click();
        this.add_whale();
    }

    public void btn_delete_one()
    {
        //this.carrot.ads.show_ads_Interstitial();
        if (this.list_fish.Count <=0) return;
        this.carrot.play_sound_click();
        int index_last = this.list_fish.Count - 1;
        this.create_effect(this.list_fish[index_last].transform.position, 0);
        Destroy(this.list_fish[index_last]);
        this.list_fish.RemoveAt(index_last);
        this.txt_count_fish.text = "x " + this.list_fish.Count;
    }

    public void btn_clear_all_whale()
    {
        this.ads.ShowInterstitialAd();
        this.carrot.play_sound_click();
        this.carrot.play_vibrate();
        for (int i = 0; i < this.list_fish.Count; i++) Destroy(this.list_fish[i]);
        this.list_fish = new List<GameObject>();
        this.txt_count_fish.text = "x " + this.list_fish.Count;
    }

    public void btn_setting()
    {
        Carrot.Carrot_Box box_setting=this.carrot.Create_Setting();
        box_setting.set_act_before_closing(after_close_setting);

        Carrot.Carrot_Box_Item setting_ram_info = box_setting.create_item_of_top("info_ram");
        setting_ram_info.set_icon(this.sp_icon_ram);
        setting_ram_info.set_title("Ram Information");
        setting_ram_info.set_tip("Hide or show ram information");
        setting_ram_info.set_act(act_change_status_info_ram);
        this.setting_ram_info_status = setting_ram_info.create_item();
        if (this.is_info_ram)
            this.setting_ram_info_status.set_icon(this.sp_icon_on);
        else
            this.setting_ram_info_status.set_icon(this.sp_icon_off);
        this.setting_ram_info_status.set_color(this.carrot.color_highlight);

        Carrot.Carrot_Box_Item setting_gpu_info=box_setting.create_item_of_top("info_gpu");
        setting_gpu_info.set_icon(this.sp_icon_cpu);
        setting_gpu_info.set_title("Advanced Information");
        setting_gpu_info.set_tip("Hide or show advanced information");
        setting_gpu_info.set_act(act_change_status_info_advanced);
        this.setting_gpu_info_status=setting_gpu_info.create_item();
        if (this.is_info_advanced)
            this.setting_gpu_info_status.set_icon(this.sp_icon_on);
        else
            this.setting_gpu_info_status.set_icon(this.sp_icon_off);
        this.setting_gpu_info_status.set_color(this.carrot.color_highlight);

        this.is_update_rank = false;
    }

    private void act_change_status_info_advanced()
    {
        if (this.is_info_advanced)
        {
            this.is_info_advanced = false;
            this.setting_gpu_info_status.set_icon(this.sp_icon_off);
            PlayerPrefs.SetInt("is_info_advanced", 1);
            this.carrot.Show_msg("Advanced Information", "Expansion information is not displayed");
        }
        else
        {
            this.is_info_advanced = true;
            this.setting_gpu_info_status.set_icon(this.sp_icon_on);
            PlayerPrefs.SetInt("is_info_advanced", 0);
            this.carrot.Show_msg("Advanced Information", "Expanded information has been displayed on the application home screen");
        }
        this.carrot.play_sound_click();
        this.check_status_info_advanced();
    }

    private void act_change_status_info_ram()
    {
        if (this.is_info_ram)
        {
            this.is_info_ram = false;
            this.setting_ram_info_status.set_icon(this.sp_icon_off);
            PlayerPrefs.SetInt("is_info_ram", 1);
            //this.carrot.show_msg("Ram Information", "Ram information is not displayed");
        }
        else
        {
            this.is_info_ram = true;
            this.setting_ram_info_status.set_icon(this.sp_icon_on);
            PlayerPrefs.SetInt("is_info_ram", 0);
            //this.carrot.show_msg("Ram Information", "Ram information has been displayed on the application home screen");
        }
        this.carrot.play_sound_click();
        this.check_status_info_ram();
    }

    private void check_status_info_advanced()
    {
        if (this.is_info_advanced)
        {
            this.g.AdvancedModuleState = GraphyManager.ModuleState.FULL;
            this.panel_advanced.SetActive(true);
        }
        else
        {
            this.g.AdvancedModuleState = GraphyManager.ModuleState.OFF;
            this.panel_advanced.SetActive(false);
        }
            
    }

    private void check_status_info_ram()
    {
        if (this.is_info_ram)
        {
            this.g.RamModuleState = GraphyManager.ModuleState.FULL;
            this.panel_ram.SetActive(true);
        }
        else
        {
            this.g.RamModuleState = GraphyManager.ModuleState.OFF;
            this.panel_ram.SetActive(false);
        }  
    }

    private void after_close_setting(List<string> list_change)
    {
        foreach (string s in list_change)
        {
            if (s == "list_bk_music") this.carrot.game.load_bk_music(this.sound_bk);
        }

        if (this.carrot.get_status_sound())
        {
            this.sound_bk.Play();
            this.panel_audio.SetActive(true);
        }
        else
        {
            this.panel_audio.SetActive(false);
            this.sound_bk.Stop();
        }
        this.is_update_rank = true;
        this.check_status_info_advanced();
        this.check_status_info_ram();
    }

    public void btn_show_user()
    {
        this.is_update_rank = false;
        this.carrot.user.show_login(this.update_rank);
    }

    public void btn_show_rank()
    {
        this.is_update_rank = false;
        this.carrot.game.Show_List_Top_player();
    }

    public void btn_show_rate()
    {
        this.is_update_rank = false;
        this.carrot.show_rate();
    }

    public void btn_show_share()
    {
        this.is_update_rank = false;
        this.carrot.show_share();
    }

    public void check_rotate_scene()
    {
        this.carrot.delay_function(1.2f,this.act_rotate_scene);
    }

    private void act_rotate_scene()
    {
        bool is_portrait = this.GetComponent<Carrot.Carrot_DeviceOrientationChange>().Get_status_portrait();

        if (is_portrait)
        {
            this.panel_fps.transform.SetParent(this.area_portrait);
            this.panel_ram.transform.SetParent(this.area_portrait);
            this.panel_fish.transform.SetParent(this.area_portrait);
            this.panel_audio.transform.SetParent(this.area_portrait);
            this.panel_advanced.transform.SetParent(this.area_portrait);
        }
        else
        {
            this.panel_fps.transform.SetParent(this.area_landspace);
            this.panel_ram.transform.SetParent(this.area_landspace);
            this.panel_fish.transform.SetParent(this.area_landspace);
            this.panel_advanced.transform.SetParent(this.area_landspace);
            this.panel_audio.transform.SetParent(this.area_landspace);
        }

        this.check_status_info_advanced();
        this.check_status_info_ram();
    }

    private void create_effect(Vector3 pos,int index_effect = 0)
    {
        GameObject obj_effect = Instantiate(this.obj_effect_prefab[index_effect]);
        obj_effect.transform.SetParent(this.arean_whale);
        obj_effect.transform.position = pos;
        obj_effect.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        Destroy(obj_effect, 1f);
    }
}
