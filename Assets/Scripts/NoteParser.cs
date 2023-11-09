
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public struct NoteInfo
{
    public NoteInfo(string button, float time) {
        song_time = time;
        button_name = button;
    }
    public float song_time;
    public string button_name;

    public override string ToString() {
        return "Time: " + song_time + " Name: " + button_name;
    }
}

public class NoteParser : MonoBehaviour
{
    // Start is called before the first frame update
    public List<NoteInfo> notes;
    public NoteParser(TextAsset note_list)     {
        notes = new List<NoteInfo>();
        List<string> lines = new List<string>(note_list.ToString().Split('\n'));
        lines.Remove("");
        lines.Remove("\n");



        foreach (string s in lines) {
            string[] info = s.Split('\t');
            notes.Add(new NoteInfo(info[2].Trim(), float.Parse(info[0].Trim())));

        }
    }





}
