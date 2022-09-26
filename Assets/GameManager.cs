using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playing = false;
    public float hpMultiplyer = 1;

    public List<Enemy> enemysOnField = new List<Enemy>();

    [SerializeField] TMP_Text money;
    [SerializeField] TMP_Text kills;
    [SerializeField] Button cannonTower;
    [SerializeField] TMP_Text cannonTowerText;
    [SerializeField] Button mgTower;
    [SerializeField] TMP_Text mgTowerText;
    [SerializeField] Tower cannonTowerPrefab;
    [SerializeField] Tower mgTowerPrefab;
    [SerializeField] int cash;

    private Tower currentPlacingTower = null;

    private int score;
    private bool placing = false;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;

        playing = true;
        cannonTowerText.text = "Cannon - " + cannonTowerPrefab.cost;
        mgTowerText.text = "MG - " + mgTowerPrefab.cost;
        money.text = cash + " $";
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlacingTower != null)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity,LayerMask.GetMask("Plane")))
            {
                currentPlacingTower.transform.position = hit.point;
            }
            if (Input.GetMouseButtonDown(0) && CheckArea() && placing)
            {
                placing = false;
                Buy(currentPlacingTower);
            }
        }
    }

    public void Score(int points)
    {
        score += points;
        cash += 10;
        money.text = cash + " $";
        kills.text = score + " Kills";
    }

    public void TakeTower(Tower t)
    {
        currentPlacingTower = Instantiate(t, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), t.transform.rotation);
        placing = true;
    }

    public void Buy(Tower t)
    {
        if(cash - t.cost >= 0)
        {
            cash -= t.cost;
            money.text = cash + " $";
            currentPlacingTower.active = true;
            currentPlacingTower = null;
        }
    }

    public bool CheckArea()
    {
        return true;
    }

    public void GameOver()
    {
        playing = false;
    }
}
