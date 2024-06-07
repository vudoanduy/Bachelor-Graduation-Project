using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// Nguoi choi se co 2 loai HP: hp goc va hp skin (sau nay co the mo rong thanh hp tu cac nguon khac vv)
// Luong mau goc cua nguoi choi co the thay doi khi hoan thanh nhiem vu (sau nay co the se nang cap sang he thong tang level)
// Luong mau chinh thuc se la tong cua hp goc va hp skin
// Hien tai nguoi choi chi co 1 thong so duy nhat la hp (sau nay se nang cap them dame,v.v....)

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] GameObject shieldImg;

    TextMeshProUGUI hpInfo;
    Image hpBar;
    ManageSkin manageSkin;
    Animator anim;

    SpawnPlayer spawnPlayer;

    bool isGetDamage = true;

    protected int hpBase = 1, hpSkin, hpPlayer, hpCurrent = 0; // hpPlayer = hpBase + hpSkin;
    protected int damageBase = 1;
    protected int timeImmortalItem, timeImmortalAfterHit = 1;

    private float damageCoefficient = 1;
    private int defaultDamageBase;

    private void Start(){
        manageSkin = GameObject.Find("ManageSkin").GetComponent<ManageSkin>();
        anim = this.GetComponent<Animator>();
        spawnPlayer = FindObjectOfType<SpawnPlayer>();
        spawnPlayer.SetStateRevivalBtn(false);
        
        timeImmortalItem = manageSkin.ReadTimeImmortalSkin();
        hpSkin = manageSkin.ReadHpSkinCurrent();
        hpCurrent = this.hpSkin + this.hpBase;
        defaultDamageBase = damageBase;

        UpdateHPPlayer();
    }

    #region Set Parameters

    public void SetHPBase(int hpBase){
        this.hpBase = hpBase;
        UpdateHPPlayer();
    }

    public void SetHPSkin(int hpSkin){
        this.hpSkin = hpSkin;
        UpdateHPPlayer();
    }

    public void SetDamageBase(int damageBase){
        this.damageBase = damageBase;
        this.defaultDamageBase = damageBase;
    }

    public void SetHPCurrent(int hpCurrent){
        this.hpCurrent = hpCurrent;
        UpdateHPPlayer();
    }

    #endregion

    // Khi nhan dame tu 1 nguon nao do se tru luong hp hien tai va cap nhat gia tri len
    // Neu hp hien tai giam ve 0, nguoi choi se chet
    #region Get Damage From Others

    public void GetDame(int damage){
        if(isGetDamage){
            this.hpCurrent -= (int)(damage * damageCoefficient);
            if(this.hpCurrent <= 0){
                this.hpCurrent = 0;
            }
            UpdateHPInfo();
            if(this.hpCurrent == 0){
                PlayAnimDisappearPlayer();
            } else {
                StartCoroutine(PlayAnimAfterGetDamage(timeImmortalAfterHit));
            }
        }
    }

    public void PlayAnimDisappearPlayer(){
        StartCoroutine(PlayAnimDisappear());
    }

    IEnumerator PlayAnimDisappear(){
        Rigidbody2D rbPlayer = GetComponent<Rigidbody2D>();
        
        Destroy(this.GetComponent<PlayerMove>());
        anim.Play("DisappearPlayer");
        rbPlayer.simulated = false;
        yield return new WaitForSeconds(1.167f);

        this.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        spawnPlayer.SetStateRevivalBtn(true);
        Destroy(this.gameObject);
        yield break;
    }

    #endregion

    #region Update Value

    public void UpdateHPPlayer(){
        this.hpPlayer = this.hpSkin + this.hpBase;
        if(this.hpCurrent > this.hpPlayer){
            this.hpCurrent = this.hpPlayer;
        }
        UpdateHPInfo();
    }

    // Neu nguoi choi co cac thao tac thay doi luong hp thi se cap nhat theo gia tri hp do
    public void UpdateHPInfo(){
        if(hpInfo == null){
            hpInfo = GameObject.Find("HpInfo").GetComponent<TextMeshProUGUI>();
            hpBar = GameObject.Find("HPBar").GetComponent<Image>();
        }
        hpInfo.text = this.hpCurrent + "/" + this.hpPlayer;
        hpBar.fillAmount = (float)this.hpCurrent/(float)this.hpPlayer;
    }

    #endregion

    #region Get Info
    public int GetHpCurrent(){
        return this.hpCurrent;
    }

    public int GetHPPlayer(){
        return this.hpPlayer;
    }

    public int GetDamageBase(){
        return this.damageBase;
    }

    #endregion

    #region Consider Item Used

    // Hoi 1 luong mau theo % hp cua lo mau
    public void HealHP(float percentHP){
        float hpHeal = (float)this.hpPlayer * percentHP;
        this.hpCurrent += (int)hpHeal;
        UpdateHPPlayer();
    }

    // Hoi 1 luong mau theo hp co dinh cua lo mau
    public void HealHP(int healHP){
        this.hpCurrent += healHP;
        UpdateHPPlayer();
    }

    // Su dung item bat tu
    public void UseImmortalItem(){
        StartCoroutine(Immortal(timeImmortalItem));
    }
    
    // Giam sat thuong nhan vao
    public void SetDamageCoefficient(float damageCoefficient,float timeEffect){
        this.damageCoefficient = damageCoefficient;
        Invoke(nameof(DefaultDamageCoefficient), timeEffect);
    }

    public void DefaultDamageCoefficient(){
        this.damageCoefficient = 1;
    }

    // Tang damage gay ra
    public void IncreaseDamageBase(int damage, float timeEffect){
        this.damageBase += damage;
        Invoke(nameof(DefaultDamageBase), timeEffect);
    }

    public void DefaultDamageBase(){
        this.damageBase = defaultDamageBase;
    }
    
    // Goi toi ham bat tu voi thoi gian duoc xet
    IEnumerator Immortal(float timeImmortal){
        isGetDamage = false;
        shieldImg.SetActive(true);

        yield return new WaitForSeconds(timeImmortal);

        isGetDamage = true;
        shieldImg.SetActive(false);
        yield break;
    }

    IEnumerator PlayAnimAfterGetDamage(float timeImmortal){
        anim.SetTrigger("isHit");
        anim.SetBool("is Hit", true);

        StartCoroutine(Immortal(timeImmortal));
        yield return new WaitForSeconds(timeImmortal);

        anim.SetBool("is Hit", false);
        yield break;
    }

    #endregion

}
