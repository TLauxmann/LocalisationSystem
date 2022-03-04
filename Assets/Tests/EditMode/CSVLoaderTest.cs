using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoaderTest
{
    private static string fileName = "TestFiles/localisationTestCSV";

    [Test]
    public void DictionaryValues()
    {
        CSVLoader csvLoader = new CSVLoader(fileName);
        Dictionary<string, string> dictionaryEN = csvLoader.GetDictionaryValues("en");
        dictionaryEN.TryGetValue("hello_world", out string helloWorld);
        Assert.AreEqual("Hello world!", helloWorld);
        Assert.AreEqual(3, dictionaryEN.Values.Count);

        Dictionary<string, string> dictionaryFR = csvLoader.GetDictionaryValues("fr");
        dictionaryFR.TryGetValue("hello_world", out string bonjourLeMonde);
        Assert.AreEqual("Bonjour le monde", bonjourLeMonde);


    }

}
