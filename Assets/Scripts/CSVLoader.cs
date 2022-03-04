using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader
{
    //Reference file;
    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char surround = '"';
    private char fieldSeperator =  ';';

    public CSVLoader(string fileNameInResourceFolder)
    {
        csvFile = Resources.Load<TextAsset>(fileNameInResourceFolder);
    }
    public Dictionary<string, string> GetDictionaryValues(string attributeId)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeperator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeperator);

        for(int i=0; i<headers.Length; i++)
        {
            if (headers[i].Contains(attributeId))
            {
                attributeIndex = i;
                break;
            }
        }

        if(attributeIndex == -1)
        {
            Debug.LogError("Could not find language attributeId " + attributeId);
        }
        else
        {
            Regex CSVParser = new Regex(fieldSeperator + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for(int i=1; i<lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = CSVParser.Split(line);

                //preventing empty or corrupted lines
                if(fields.Length > attributeIndex)
                {
                    fields[attributeIndex] = fields[attributeIndex].TrimStart(' ', surround);
                    fields[attributeIndex] = fields[attributeIndex].TrimEnd(surround);
                
                    var key = fields[0];

                    if (dictionary.ContainsKey(key)) { continue; }

                    var value = fields[attributeIndex];

                    dictionary.Add(key, value);
                }
            }
        }

        return dictionary;
    }

}
