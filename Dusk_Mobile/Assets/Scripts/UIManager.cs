using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Status 패널UI GameObject
    public GameObject statusPanel;
    //LevelUP Panel UI Game Object
    public GameObject LevelUpPanel;

    public TextMeshProUGUI abilityTextTitle;

    //Time in Stage1
    public TextMeshProUGUI time;

    [Header("LeftAbilityPanel")]
    public TextMeshProUGUI LeftAbilityTitle;
    public Image LeftAbilityImg;
    public TextMeshProUGUI LeftAbilityExplanation;

    [Header("RightAbilityPanel")]
    public TextMeshProUGUI RightAbilityTitle;
    public Image RightAbilityImg;
    public TextMeshProUGUI RightAbilityExplanation;

    [Header("MainGamePlayUI")]
    public TextMeshProUGUI playerHPText;
    public Slider healthBar;

    public TextMeshProUGUI playerExpText;
    public Slider expBar;

    [Header("PlayerStats")]
    //Left Player Info
    public TextMeshProUGUI playerName;
    public Image playerImg;
    public TextMeshProUGUI playerLevel;

    //PlayerStats의 현재 플레이어 스탯 현황
    public TextMeshProUGUI HealthStatText;
    public Slider hp_Status_Bar;
    public TextMeshProUGUI PowerStatText;
    public Slider power_Status_Bar;
    public TextMeshProUGUI SpeedStatText;
    public Slider speed_Status_Bar;
    //현재 플레이어 레벨
    public TextMeshProUGUI LEVEL;

    private float maxDamage = 30f;
    private string targetTag = "Player"; // 찾고자 하는 태그
    private int before_level = 1;



    //Select Ability의 설명문에 쓰일 문구
    //HP, Power, AtkSpd, Spd 순서
    private string[] UpgradeIndex = {
        "The character's health increases by ",
        "The player's attack power increases by ",
        "The character's attack speed increases by ",
        "The character's movement speed increases by "
    };
    // HP, Power, Attack Speed, Speed
    private string[] UpgradeTitle = { "HP Upgrade", "Power Upgrade", "Attack Speed Upgrade", "Speed Upgrade" };

    /*
     * Stat에서 사용할 이미지
     * "HP Upgrade", "Power Upgrade", "Attack Speed Upgrade", "Speed Upgrade"
     */
    public Sprite[] HK_Img;
    public Sprite[] king_Img;
    public Sprite[] trd_Img;

    public GameObject gameOverPanel;

    private GameObject player;
    private CharacterStats characterStats;
    private CharacterManager characterManager;
    private int[] selectedIndices;
    private float countTime = 0;


    void Awake()
    {
        statusPanel.SetActive(false);
        LevelUpPanel.SetActive(false);
    }
    void Start()
    {
        // Player 태그를 가진 게임 오브젝트 찾기
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        // 찾은 게임 오브젝트에 대한 처리
        foreach (GameObject obj in objectsWithTag)
        {
            // 찾은 게임 오브젝트에 대한 로직을 추가
            if (obj.tag == targetTag)
            {
                player = obj;
                break;
            }
        }

        characterStats = player.GetComponent<CharacterStats>();
        characterManager = player.GetComponent<CharacterManager>();
        Status_Bar();
        //플레이어 이름으로 수정
        playerName.text = player.name;
        //플레이어 SpriteImage가져오기
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        //Image에 PlayerSpriteImage 넣기
        playerImg.sprite = playerSprite.sprite;

        if(SceneManager.GetActiveScene().name == "Stage2")
        {
            characterStats.curHealth = SceneManagerEX.Instance.health;
        }
    }
    void FixedUpdate()
    {
        SceneManagerEX.Instance.totalTime +=Time.deltaTime;
        time.text = ((int)SceneManagerEX.Instance.totalTime / 60 % 60).ToString() + ":"+ ((int)SceneManagerEX.Instance.totalTime % 60).ToString();
        
    }
    void Update()
    {
        //Player에서 Level 하고 experience값 가져오기.
        //playerLevel.text = "Level: "+player

        // 아래 둘은 일단 Update에 배치했지만 후에 체력과 스탯이 상호작용하는 곳에 연결해놓고 테스트
        //플레이어 체력 설정
        HealthBar();
        //플레이어 경험치바 설정
        ExpBar();
        //플레이어 스탯 설정
        Status_Bar();
    }
    public void OnClickStatus()
    {
        Time.timeScale = 0;
        statusPanel.SetActive(true);
    }

    public void OnClickCancel()
    {
        Time.timeScale = 1;
        statusPanel.SetActive(false);
    }

    void HealthBar()
    {
        //플레이어의 체력을 갱신 (플레이어 체력이 왔다갔다 하는 곳에 배치)
        healthBar.value = ((float)characterStats.curHealth / (float)characterStats.maxHealth);
        //Debug.Log("PlayerHealthValue"+healthBar.value);
        //Debug.Log("PlayerHealth: " + characterStats.curHealth + "/" + characterStats.maxHealth);
        playerHPText.text = characterStats.curHealth.ToString() + "/" + characterStats.maxHealth.ToString();

        if(characterStats.curHealth <= 0)
        {
            Invoke("ActiveGameOverPanel", 2f);
        }
    }

    void ExpBar()
    {
        //level: 플레이어 현재 레벨
        //exp: 플레이어 현재 경험치 찬 정도
        //추가사항 풀 받고 작업 진행
        
        LEVEL.text = SceneManagerEX.Instance.player_level.ToString();
        expBar.value = (float)(SceneManagerEX.Instance.player_Exp%20) / 20.0f;
        playerExpText.text = (SceneManagerEX.Instance.player_Exp % 20).ToString();
        playerLevel.text = "Level: " + LEVEL.text +" "+((float)(SceneManagerEX.Instance.player_Exp) / (float)20.0f) * 100 + "%";
        //PlayerLEvelUp!
        if (SceneManagerEX.Instance.player_Exp >= 20)
        {
            SceneManagerEX.Instance.player_level++;
            SceneManagerEX.Instance.player_Exp -= 20;
            OnClickExpBtn();
        }
    }

    void Status_Bar()
    {
        hp_Status_Bar.value = ((float)characterStats.maxHealth / 300.0f);
        //power_Status_Bar = ((float)characterStats.damage.GetStat() / (float)characterStats.maxDamage);
        power_Status_Bar.value = ((float)characterStats.damage.GetStat() / maxDamage);
        //speed_Status_Bar.value = ((float)(characterManager.m_speed) / (float)characterStats.maxSpeed);
        //speed_Status_Bar.value = (float)characterManager.GetSpeed() - SpeedOffset;
        speed_Status_Bar.value = characterManager.GetSpeed()/ 8.0f;

        HealthStatText.text = characterStats.maxHealth.ToString() + "/" + "300";
        PowerStatText.text = characterStats.damage.GetStat().ToString() + "/" + "30";
        SpeedStatText.text = characterManager.GetSpeed().ToString() + " /"+"8.0";
    }
    
    void StatSelect()
    {
        abilityTextTitle.text = "Level: "+((SceneManagerEX.Instance.player_Exp/20)+1).ToString() + " Select Ability";
        //2개의 선택된 index 반환
        selectedIndices = SelectNonZeroIndices(SceneManagerEX.Instance.max_Status);

        //선택된 두 개의 값의 정보를 Panel에 넣기
        if(selectedIndices.Length >= 2)
        {
            LeftAbilityTitle.text = UpgradeTitle[selectedIndices[0]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[0]]).ToString();
            if(SceneManagerEX.Instance.selectChar.name  == "HeroKnight")
            {
                LeftAbilityImg.sprite = HK_Img[selectedIndices[0]];
            }
            else if(SceneManagerEX.Instance.selectChar.name == "Hero3rd")
            {
                LeftAbilityImg.sprite = trd_Img[selectedIndices[0]];
            }
            else if(SceneManagerEX.Instance.selectChar.name == "HeroKing")
            {
                LeftAbilityImg.sprite = king_Img[selectedIndices[0]];
            }
            LeftAbilityExplanation.text = UpgradeIndex[selectedIndices[0]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[0]]).ToString();

            RightAbilityTitle.text = UpgradeTitle[selectedIndices[1]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[1]]).ToString();
            if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
            {
                RightAbilityImg.sprite = HK_Img[selectedIndices[1]];
            }
            else if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
            {
                RightAbilityImg.sprite = trd_Img[selectedIndices[1]];
            }
            else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
            {
                RightAbilityImg.sprite = king_Img[selectedIndices[1]];
            }
            RightAbilityExplanation.text = UpgradeIndex[selectedIndices[1]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[1]]).ToString();
        }
        else if(selectedIndices.Length == 1)
        {
            LeftAbilityTitle.text = UpgradeTitle[selectedIndices[0]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[0]]).ToString();
            if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
            {
                LeftAbilityImg.sprite = HK_Img[selectedIndices[0]];
            }
            else if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
            {
                LeftAbilityImg.sprite = trd_Img[selectedIndices[0]];
            }
            else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
            {
                LeftAbilityImg.sprite = king_Img[selectedIndices[0]];
            }
            LeftAbilityExplanation.text = UpgradeIndex[selectedIndices[0]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[0]]).ToString();

            RightAbilityTitle.text = UpgradeTitle[selectedIndices[0]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[0]]).ToString();
            if (SceneManagerEX.Instance.selectChar.name == "HeroKnight")
            {
                LeftAbilityImg.sprite = HK_Img[selectedIndices[0]];
            }
            else if (SceneManagerEX.Instance.selectChar.name == "Hero3rd")
            {
                LeftAbilityImg.sprite = trd_Img[selectedIndices[0]];
            }
            else if (SceneManagerEX.Instance.selectChar.name == "HeroKing")
            {
                LeftAbilityImg.sprite = king_Img[selectedIndices[0]];
            }
            RightAbilityExplanation.text = UpgradeIndex[selectedIndices[0]] + " " + (6 - SceneManagerEX.Instance.max_Status[selectedIndices[0]]).ToString();
        }
    }
    // HP, Power, Attack Speed, Speed
    public void OnCllickLeft()
    {
        SceneManagerEX.Instance.max_Status[selectedIndices[0]]--;
        switch (selectedIndices[0])
        {
            case 0:
                characterStats.maxHealth += 50;
                characterStats.curHealth += 50;
                break;
            case 1:
                characterStats.damage.SetStat(characterStats.damage.GetStat()+5);
                break;
            case 2:
                break;
            case 3:
                characterManager.SetSpeed(characterManager.GetSpeed()+1);
                break;
        }
        Debug.Log($"Updated Array after decrement at index {0}: [{string.Join(", ", SceneManagerEX.Instance.max_Status)}]");
        LevelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickRight()
    {
        if (selectedIndices.Length == 1)
        {
            SceneManagerEX.Instance.max_Status[selectedIndices[0]]--;
            switch (selectedIndices[0])
            {
                case 0:
                    characterStats.maxHealth += 50;
                    characterStats.curHealth += 50;
                    break;
                case 1:
                    characterStats.damage.SetStat(characterStats.damage.GetStat() + 5);
                    break;
                case 2:
                    break;
                case 3:
                    characterManager.SetSpeed(characterManager.GetSpeed() + 1);
                    break;
            }
        }
            
        else if (selectedIndices.Length == 2)
        {
            SceneManagerEX.Instance.max_Status[selectedIndices[1]]--;
            switch (selectedIndices[1])
            {
                case 0:
                    characterStats.maxHealth += 50;
                    characterStats.curHealth += 50;
                    break;
                case 1:
                    characterStats.damage.SetStat(characterStats.damage.GetStat()+5);
                    break;
                case 2:
                    break;
                case 3:
                    characterManager.SetSpeed(characterManager.GetSpeed() + 1);
                    break;
            }
        }
            

        Debug.Log($"Updated Array after decrement at index {1}: [{string.Join(", ", SceneManagerEX.Instance.max_Status)}]");
        LevelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    int[] SelectNonZeroIndices(int[] array)
    {
        System.Collections.Generic.List<int> nonZeroIndices = new System.Collections.Generic.List<int>();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != 0)
            {
                nonZeroIndices.Add(i);
            }
        }

        if (nonZeroIndices.Count == 1)
        {
            return new int[] { nonZeroIndices[0] };
        }
        else if (nonZeroIndices.Count >= 2)
        {
            
            int randomIndex1 = UnityEngine.Random.Range(0, nonZeroIndices.Count);
            int randomIndex2;
            do
            {
                randomIndex2 = UnityEngine.Random.Range(0, nonZeroIndices.Count);
            }while(randomIndex1==randomIndex2);

            return new int[] { nonZeroIndices[randomIndex1], nonZeroIndices[randomIndex2] };
        }
        else
        {
            return new int[0];
        }
    }
    public void OnClickExpBtn()
    {
        Time.timeScale = 0;
        StatSelect();
        LevelUpPanel.SetActive(true);
    }

    public void OnClickRe()
    {
        SceneManagerEX.Instance.LoadMainMenu();
    }

    public void OnClickExit()
    {
        SceneManagerEX.Instance.ExitGame();
    }

    public void ActiveGameOverPanel()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
