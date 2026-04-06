using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MazeManager : MonoBehaviour
{
    public int[] mazeCells; 
    private int currentIndex = 0;   // index for the cell
    public Text m_MyText;
    MazeCell[] allMazeCells;
    
    private bool Started = false;
    public GameManager _gameManager;
    public Material originalMat;
    public Material newMat;
    public Material wrongMat;
    public AudioSource gridAudio;
    public List<AudioClip> audioClips;
    private int curIndex;
    public GameObject key;
    private void Start()
    {
        
        allMazeCells = FindObjectsOfType<MazeCell>();
        //ResetMaze();
    }
    void OnEnable() {
        allMazeCells = FindObjectsOfType<MazeCell>();
        
    }

    void Update()
    {
        
    }
    public void CheckCell(int cellIndex, bool isCorrectCell)
    {
        
        if(cellIndex == -1)
        {
            ShowMessage("Begin to Move ");
            Debug.Log("RESTART!");
            ResetCellColor();
            Started = true;
            PlayStartOrEndGridMusic();
            return;
        }
        GameObject currentCell = GetCellByIndex(cellIndex);
        if (cellIndex == mazeCells[currentIndex])
        {
                
            if(Started)
            {
                currentIndex++;
                Debug.Log("Correct cell! Continue to next cell..." + cellIndex + ", " + currentIndex + " " + mazeCells.Length);
                if(currentIndex == mazeCells.Length )
                {
                    // arrive the destination point
                    key.SetActive(false);
                    m_MyText.text = "";
                    Started = false;
                    _gameManager.FinishMaze();
                    PlayStartOrEndGridMusic();
                }
                else
                {
                    PlayRightGridMusic();
                }
                ChangeCellColor(currentCell, Color.green);
                //ChangeCellMat(currentCell,newMat);
                    
            }
               
                    
        }
        else
        {
                PlayRandomHorrorMusic();
                Debug.Log(currentCell.gameObject.name);
                Debug.Log("Wrong cell! Reseting..." + cellIndex);
                //ChangeCellMat(currentCell, wrongMat);
                ChangeCellColor(currentCell, Color.red);
                ShowMessage("Wrong! " + cellIndex);
                ResetMaze();
        }
        
        
    }


    private GameObject GetCellByIndex(int cellIndex)
    {
        
        foreach (MazeCell cell in allMazeCells)
        {
            if (cell.cellIndex == cellIndex)  
            {
                return cell.gameObject;
            }
        }

        return null;
    }
    private void ShowMessage(string message)
    {
       m_MyText.text = message;
    }

    private void ResetMaze()
    {
        //ResetCellMat();
        //ResetCellColor();
        currentIndex = 0;  
        Started = false;
        ShowMessage("Please go back to restart");
    }
    private void ChangeCellMat(GameObject cell, Material newMat)
    {
        Renderer cellRenderer = cell.GetComponent<Renderer>();

        if (cellRenderer != null)
        {
            cellRenderer.material = newMat;
        }
    }
    private void ChangeCellColor(GameObject cell, Color color)
    {
        Renderer cellRenderer = cell.GetComponent<Renderer>();

        if (cellRenderer != null)
        {
            cellRenderer.material.color = color;
        }
    }
    private void ResetWrongCellMat()
    {
        GameObject currentCell = GetCellByIndex(curIndex);
        Renderer cellRenderer = currentCell.GetComponent<Renderer>();

        if (cellRenderer != null)
        {
            Debug.Log("reset wrong" + currentCell.name);
            cellRenderer.material = originalMat;
        }
        
    }
    private void ResetCellMat()
    {
        for(int i = 0; i <= currentIndex + 1; i++ )
        {
            GameObject currentCell = GetCellByIndex(i);
            Renderer cellRenderer = currentCell.GetComponent<Renderer>();

            if (cellRenderer != null)
            {
                cellRenderer.material = originalMat;
            }
        }
       
        
    }
    private void ResetCellColor()
    {
        for(int i = 0; i < 12; i++ )
        {
            GameObject currentCell = GetCellByIndex(i);
            Renderer cellRenderer = currentCell.GetComponent<Renderer>();
            if (cellRenderer != null)
            {
                cellRenderer.material.color = Color.white;
            }
        }
       
        
    }

    public void PlayRightGridMusic()
    {
        gridAudio.clip = audioClips[0]; 
        gridAudio.Play(); 
    }
    public void PlayStartOrEndGridMusic()
    {
        gridAudio.clip = audioClips[1]; 
        gridAudio.Play(); 
    }
    public void PlayRandomHorrorMusic()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(2, audioClips.Count);
            gridAudio.clip = audioClips[randomIndex]; 
            gridAudio.Play(); 
        }
        else
        {
            Debug.LogWarning("Audio clip list is empty!");
        }
    }
}

