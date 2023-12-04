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
    //Status �г�UI GameObject
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

    //PlayerStats�� ���� �÷��̾� ���� ��Ȳ
    public TextMeshProUGUI HealthStatText;
    public Slider hp_Status_Bar;
    public TextMeshProUGUI PowerStatText;
    public Slider power_Status_Bar;
    public TextMeshProUGUI SpeedStatText;
    public Slider speed_Status_Bar;
    //���� �÷��̾� ����
    public TextMeshProUGUI LEVEL;

    private float maxDamage = 30f;
    private string targetTag = "Player"; // ã���� �ϴ� �±�
    private int before_level = 1;



    //Select Ability�� ������ ���� ����
    //HP, Power, AtkSpd, Spd ����
    private string[] UpgradeIndex = {
        "The character's health increases by ",
        "The player's attack power increases by ",
        "The character's attack speed increases by ",
        "The character's movement speed increases by "
    };
    // HP, Power, Attack Speed, Speed
    private string[] UpgradeTitle = { "HP Upgrade", "Power Upgrade", "Attack Speed Upgrade", "Speed Upgrade" };

    /*
     * Stat���� ����� �̹���
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
        // Player �±׸� ���� ���� ������Ʈ ã��
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        // ã�� ���� ������Ʈ�� ���� ó��
        foreach (GameObject obj in objectsWithTag)
        {
            // ã�� ���� ������Ʈ�� ���� ������ �߰�
            if (obj.tag == targetTag)
            {
                player = obj;
                break;
            }
        }

        characterStats = player.GetComponent<CharacterStats>();
        characterManager = player.GetComponent<CharacterManager>();
        Status_Bar();
        //�÷��̾� �̸����� ����
        playerName.text = player.name;
        //�÷��̾� SpriteImage��������
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        //Image�� PlayerSpriteImage �ֱ�
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
        //Player���� Level �ϰ� experience�� ��������.
        //playerLevel.text = "Level: "+player

        // �Ʒ� ���� �ϴ� Update�� ��ġ������ �Ŀ� ü�°� ������ ��ȣ�ۿ��ϴ� ���� �����س��� �׽�Ʈ
        //�÷��̾� ü�� ����
        HealthBar();
        //�÷��̾� ����ġ�� ����
        ExpBar();
        //�÷��̾� ���� ����
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
        //�÷��̾��� ü���� ���� (�÷��̾� ü���� �Դٰ��� �ϴ� ���� ��ġ)
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
        //level: �÷��̾� ���� ����
        //exp: �÷��̾� ���� ����ġ �� ����
        //�߰����� Ǯ �ް� �۾� ����
        
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
        //2���� ���õ� index ��ȯ
        selectedIndices = SelectNonZeroIndices(SceneManagerEX.Instance.max_Status);

        //���õ� �� ���� ���� ������ Panel�� �ֱ�
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
