using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class car : MonoBehaviour
{

    public GameObject box, gameover, congratulation;

    public TMP_Text soal_tampil, skor_tampil;

    public float speed, sudut;

    public string [] soal, kuncijawaban;//80|23|45

    string[] jawaban;

    int nomor = -1;

    int skor = 0;

    void Start(){
        StartCoroutine(lanjutsoal());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0,0);
            transform.rotation = Quaternion.Euler(0,-10,0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(speed, 0,0);
            transform.rotation = Quaternion.Euler(0,10,0);
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0,0);
            transform.rotation = Quaternion.Euler(0,0,0);
        }

    }

    void OnTriggerEnter(Collider obj){
        if (obj.name == "box"){
            jawaban = kuncijawaban[nomor].Split('|');
            if (obj.transform.GetChild(0).GetComponent<TMP_Text>().text == jawaban[0]){
                skor += 10;
                skor_tampil.text = skor.ToString();
                GetComponent<AudioSource>().Play();
            } else{
                gameover.SetActive(true);
                Time.timeScale = 0;

            }
            for(int i=0;i<obj.transform.parent.childCount; i++)
            {
                obj.transform.parent.GetChild(i).GetComponent<BoxCollider>().enabled = false;

            }
            obj.gameObject.SetActive(false);
            StartCoroutine(lanjutsoal());
        }
    }

    IEnumerator lanjutsoal(){
        yield return new WaitForSeconds(1.5f);

        nomor++;

        if (nomor < soal.Length)
        {
            soal_tampil.transform.parent.gameObject.SetActive(true);
            soal_tampil.text = soal[nomor];

            box.GetComponent<Animator>().enabled = true;
            box.GetComponent<Animator>().Play(0);

            jawaban = kuncijawaban[nomor].Split('|');
            for (int i = 0; i < box.transform.childCount; i++)
            {
                box.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = "";
                box.transform.GetChild(i).gameObject.SetActive(true);
                box.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            }
            int index = 0;
            for (int i = 0; i < box.transform.childCount; i++)
            {
                do
                {
                    index = (int)Random.Range(0, 2.4f);
                } while (box.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text != "");
                box.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text = jawaban[i];
            }
        }

        else
        {
            Time.timeScale = 0;
            congratulation.SetActive(true);

        }
    }

    public void restart(){
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevelName);

    }

}
