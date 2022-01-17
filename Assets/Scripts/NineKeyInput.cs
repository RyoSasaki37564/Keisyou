using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NineKeyInput : MonoBehaviour
{
    [SerializeField]Text m_dialog = default;

    [SerializeField] GameObject[] m_commands = new GameObject[9];


    public struct CommandCode
    {
        public int Number { get; set; }
        public int Contact { get; set; }

        /// <summary>
        /// 入力されたキーの番号と接触方向を受け取り、ID情報に変換する
        /// </summary>
        /// <param name="numID">キー番号</param>
        /// <param name="conID">接触方向</param>
        public CommandCode(int numID, int conID)
        {
            Number = numID;
            Contact = conID;
        }
    }
    List<CommandCode> m_commandList = new List<CommandCode>(); //ここに格納された値を参照し、該当する龍撃の演出を呼び出す。

    bool m_phase = false;

    void Update()
    {
        if(m_phase == false)
        {
            if (Input.GetButton("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider)
                {
                    Debug.Log("接触情報：" + hit.collider.gameObject.transform.parent.name + " " + hit.collider.gameObject.name);

                    CommandCode m_CC = new CommandCode(int.Parse(hit.collider.gameObject.transform.parent.name),
                        int.Parse(hit.collider.gameObject.name));

                    hit.collider.gameObject.transform.parent.gameObject.SetActive(false);

                    m_commandList.Add(m_CC);
                }
            }
        }
    }

    public void Phaser()
    {
        if(m_phase == false)
        {
            m_phase = true;
            Ryuugeki(m_commandList);
            foreach (var i in m_commands)
            {
                i.SetActive(true);
            }

        }
        else
        {
            m_phase = false;
            m_dialog.text = "";
            foreach (var i in m_commands)
            {
                i.SetActive(true);
            }
        }
    }

    public void Ryuugeki(List<CommandCode> commands)
    {
        if(commands.Count == 3)
        {
            if(commands[0].Number == 2 && commands[1].Number == 5 && commands[2].Number == 8)
            {
                m_dialog.text = "顎門落とし";
            }
            else
            {
                m_dialog.text = "ガチビンタ";
            }
        }
        else if(commands.Count == 9)
        {
            if (commands[0].Number == 1 &&
                commands[1].Number == 2 &&
                commands[2].Number == 3 &&
                commands[3].Number == 6 &&
                commands[4].Number == 9 &&
                commands[5].Number == 8 &&
                commands[6].Number == 7 &&
                commands[7].Number == 4 &&
                commands[8].Number == 5)
            {
                m_dialog.text = "とぐろ回し";
            }
            else
            {
                m_dialog.text = "ガチビンタ";
            }
        }
        else
        {
            m_dialog.text = "ガチビンタ";
        }

        commands.Clear();
    }
}