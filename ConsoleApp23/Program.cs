using System;
using System.Collections.Generic;

public class Program
{

public static void Main(string[] args)
{
    // ======================= TESTS (do not change) =======================
    Console.WriteLine("== A2 Summarize ==");
    var s = Summarize(new[] { 84, 62, 98, 75, 90 });
    Console.WriteLine($"{s.Min} {s.Max} {s.Average}");
    var (low, high, avg) = Summarize(new[] { 50 });
    Console.WriteLine($"{low} {high} {avg}");
    Console.WriteLine();
    Console.WriteLine("== B2 HashTable ==");
    var ht = new HashTable();
    ht.Set("nails", 100); ht.Set("tile", 50); ht.Set("lumber", 80);
    ht.Set("bolts", 0); ht.Set("screws", 140);
    Console.WriteLine(ht.ContainsKey("lumber"));
    Console.WriteLine(ht.ContainsKey("glue"));
    Console.WriteLine(ht.TryGet("bolts", out int v1) + " " + v1);
    Console.WriteLine(ht.TryGet("glue", out int v2) + " " + v2);
    Console.WriteLine();
    Console.WriteLine("== B3 MostFrequentWord ==");
    Console.WriteLine(MostFrequentWord("the cat and the hat and the bat"));
    Console.WriteLine(MostFrequentWord("Red blue RED green red"));
    Console.WriteLine();
    Console.WriteLine("== B3 CommonNames ==");
    var both = CommonNames(new[] { "Ava", "Leo", "Maya", "Sam" },
        new[] { "Leo", "Sam", "Zoe" });
    var list = new List<string>(both);
    list.Sort(); // sort only so the output is the same every time
    Console.WriteLine(string.Join(", ", list));
    Console.WriteLine();
}

static (int Min, int Max, double Average) Summarize(int[] scores)
{
    int minimunb = scores[0];
    int maximo = scores[0];
    double suma = 0;

    for (int i = 0; i < scores.Length; i++)
    {
        if (scores[i] < minimunb)
                minimunb = scores[i];

        if (scores[i] > maximo)
                maximo = scores[i];

        suma += scores[i];
    }

    double average = suma/ scores.Length;
    return (minimunb, maximo, average);
}

static (string Word, int Count) MostFrequentWord(string text)
{
    string[] words = text.ToLower().Split(' ');
    Dictionary<string, int> counts = new Dictionary<string, int>();
    string mostWord = "";
    int mostCount = 0;

    foreach (string word in words)
    {
        if (counts.ContainsKey(word))
            counts[word]++;
        else
            counts[word] = 1;

        if (counts[word] > mostCount)
        {
            mostCount = counts[word];
            mostWord = word;
        }
    }

    return (mostWord, mostCount);
}

static HashSet<string> CommonNames(string[] first, string[] second)
{
    HashSet<string> names = new HashSet<string>(first);
    names.IntersectWith(second);
    return names;
}
}


public class HashTable
{
    private readonly int size = 7;
    private Node?[] dataMap;

    public HashTable()
    {
        dataMap = new Node?[size];
    }

    private int Hash(string key)
    {
        int hash = 0;

        foreach (char c in key)
        {
            hash = (hash + c * 23) % dataMap.Length;
        }

        return hash;
    }

    private class Node
    {
        public string Key;
        public int Value;
        public Node? Next;

        public Node(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }

    public void Set(string key, int value)
    {
        int index = Hash(key);
        Node temporal = dataMap[index];

        if (temporal == null)
        {
            dataMap[index] = new Node(key, value);
            return;
        }

        while (true)
        {
            if (temporal.Key.Equals(key))
            {
                temporal.Value = value;
                return;
            }

            if (temporal.Next == null)
                break;

            temporal = temporal.Next;
        }

        temporal.Next = new Node(key, value);
    }

    public int Get(string key)
    {
        int index = Hash(key);
        Node temporal = dataMap[index];
        while (temporal != null)
        {
            if (temporal.Key.Equals(key))
                return temporal.Value;
            temporal = temporal.Next;
        }
        return 0;
    }

    public List<string> Keys()
    {
        var allKeys = new List<string>();
        for (int i = 0; i < dataMap.Length; i++)
        {
            Node temp = dataMap[i];
            while (temp != null)
            {
                allKeys.Add(temp.Key);
                temp = temp.Next;
            }
        }
        return allKeys;
    }

    public bool ContainsKey(string key)
    {
        int index = Hash(key);
        Node temp = dataMap[index];
        while (temp != null)
        {
            if (temp.Key.Equals(key))
                return true;
            temp = temp.Next;
        }
        return false;
    }

    public bool TryGet(string key, out int value)
    {
        int index = Hash(key);
        Node temp = dataMap[index];
        while (temp != null)
        {
            if (temp.Key.Equals(key))
            {
                value = temp.Value;
                return true;
            }
            temp = temp.Next;
        }
        value = 0;
        return false;
    }




}


/* A1: PREDICT THE OUTPUT

Line 1: 77
Line 2: 88
Line 3: True
Line 4: Lab
Line 5: 3

Why do Lines 4 and 5 behave differently?

Tuples copy their values so changing e.Tag does not
change d.Tag. However, both tuples have the same List.
so adding 90 to e.The score also rotates as well.Scores.



 B1: HASH TABLE BY HAND

(a) Chaining

0: melon -> null
1: null
2: null
3: apple -> plum -> null
4: kiwi -> null
5: null
6: lime -> null

(b) Linear probing

slot 0: melon
slot 1: empty
slot 2: empty
slot 3: apple
slot 4: plum
slot 5: kiwi
slot 6: lime

*/