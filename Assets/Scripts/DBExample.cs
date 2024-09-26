using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UltraLiteDB;

public class DBExample : MonoBehaviour
{
    Text info;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("Info").GetComponent<Text>();
        DatabaseTest();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void DatabaseTest()
    {
        // Open database (or create if doesn't exist)
        var db = new UltraLiteDatabase("MyData.db");

        // Get a collection
        var collection = db.GetCollection("savegames");

        // Create a new character document
        var character = new BsonDocument();
        character["Name"] = "Codemaker2015";
        character["Level"] = 1;
        character["IsActive"] = true;

        // Insert new customer document (Id will be auto generated)
        BsonValue id = collection.Insert(character);
        // new Id has also been added to the document at character["_id"]

        // Update a document inside a collection
        character["Name"] = "Vishnu Sivan";
        collection.Update(character);

        // Insert a document with a manually chosen Id
        var character2 = new BsonDocument();
        character2["_id"] = 17;
        character2["Name"] = "Test Bob";
        character2["Level"] = 10;
        character2["IsActive"] = true;
        collection.Insert(character2);

        // Load all documents
        List<BsonDocument> allCharacters = new List<BsonDocument>(collection.FindAll());

        foreach (var row in allCharacters)
        {
            // Accessing fields using key names
            string name = row["name"];
            int level = row["level"];
            bool isActive = row["IsActive"];

            // Print out the character details
            string data = $"Character Name: {name}, Level: {level}, IsActive: {isActive}\n";
            info.text += data;
            Debug.Log(data);
        }

        // Delete something
        collection.Delete(10);

        // Upsert (Update if present or insert if not)
        collection.Upsert(character);

        // Don't forget to cleanup!
        db.Dispose();

    }
}
