//Trie implementaiton using hash map
using System;
using System.Collections.Generic;
using System.Text;

public class Trie
{
   class TrieNode
   {
     public IDictionary <char,TrieNode> Children = new Dictionary<char,TrieNode>();
	 public bool isLeaf = false;
	 public uint charCount = 0;
   }
   
   TrieNode root;
   public Trie ()
   {
     root = new TrieNode();
   }
   
   public void Add(String str)
   {
     Add(str,root,0);
   }
   
   private void Add(String str, TrieNode node, int strIndex)
   {
     node.charCount += 1;
	 if (strIndex == str.Length)
	 {
	   node.isLeaf = true;
	   return;
	 }
	 
	 TrieNode nextNode=null;
	 char ch = str[strIndex++];
	 if (node.Children.ContainsKey(ch))
	 {
	   nextNode = node.Children[ch];
	 } else {
	   nextNode = new TrieNode();
	   node.Children.Add(ch,nextNode);
	 }
	 Add(str,nextNode,strIndex);
   }
   
   public bool Search(String str)
   {
     return Search(str,root,0);
   }
   
   bool Search(String str, TrieNode node, int strIndex)
   {
     if (str.Length == strIndex && node.isLeaf)
	 {
	   return true;
	 } else if (str.Length == strIndex)
	 {
	   return false;
	 }
	 
	 char ch = str[strIndex++];
	  
	 if (node.Children.ContainsKey(ch))
	 {
	   TrieNode nextNode = node.Children[ch];
	   return Search(str,nextNode,strIndex);
	 } else{
	    return false;
	 }
	  
   }
   
   public bool Remove(String str)
   {
	   return Remove(str,root,0);
   }
   
   bool Remove(String str, TrieNode node, int strIndex)
   {
	   if (str.Length == strIndex && !node.isLeaf)
	   {
		   return false;
	   } else if (str.Length == strIndex)
	   {
		   node.isLeaf = false;
		   return true;
	   }
	   char ch = str[strIndex++];
	   if (!(node.Children.ContainsKey(ch)))
	   {
		   return false;
	   }
	   
	   TrieNode nextNode = node.Children[ch];
	   
	   if (Remove (str,nextNode,strIndex))
	   {
		   if (nextNode.charCount ==1)
		   {
		   node.Children.Remove(ch);
		   node.charCount--;
		   }
	   } else
	   {
		   return false;
	   }
	   return true;
   }

   public void GetWords(String prefix)
   {
	   if (prefix.Trim().Length >0)
	   GetWords(prefix,root,0);
   }
   private void GetWords(String prefix, TrieNode node, int strIndex)
   {
	   if (prefix.Length == strIndex)
	   {
		   printWords(prefix, node);
		   return;
	   }
	   char ch = prefix[strIndex++];
	   if (!(node.Children.ContainsKey(ch)))
		   return;
	   TrieNode nextNode = node.Children[ch];
	   GetWords(prefix,nextNode,strIndex);
   }
   
   private void printWords(String word, TrieNode node)
   {
	   if (node.charCount == 1 && node.isLeaf)
	   {
		   Console.WriteLine(word);
		   return;
	   }
	   if (node.isLeaf)
	   {
		  Console.WriteLine(word);   
	   }
	   StringBuilder newWord = new StringBuilder();
	   newWord.Append(word);
	   
	   foreach (var charcter in node.Children.Keys)
	   {
		   char ch = (char) charcter;
		   newWord.Append(ch);
		   
		   TrieNode nextNode = node.Children[ch];
		   printWords(newWord.ToString(),nextNode);
	   }
   }
   
   }

public class TestCase
{
	public static void Search(string str, bool result, bool expectedResult =true)
	{
	   if (result == expectedResult)
	     Console.WriteLine("Pass:- String found/notfound {0} ", str);
	   else
	      Console.WriteLine("Fail:- String found/notfound {0} ", str);
	}
	
	public void SimpleTrie(Trie trie)
	{
		String [] input = {"champ","check","checked","champion","cat","cow","bat","battle"};
		foreach (String str in input)
		{
			trie.Add(str);
		}
		
		foreach (String str in input)
		{
			Search(str,trie.Search(str),true);
		}
		
		Console.WriteLine();
		String temp = "ch";
		Console.WriteLine("Matching words for {0}",temp);
		trie.GetWords(temp);
		
		Console.WriteLine();
		temp = "M";
		Console.WriteLine("Matching words for {0}",temp);
		trie.GetWords(temp);
	}
}
public class Program
{
	public static void Search(string str, bool result, bool expectedResult =true)
	{
	   if (result == expectedResult)
	     Console.WriteLine("Pass:- String found/notfound {0} ", str);
	   else
	      Console.WriteLine("Fail:- String found/notfound {0} ", str);
	}
    
	public static void Main()
	{
	  Trie trie = new Trie();
	  //Add few string
	  trie.Add("Milind");
	  trie.Add("Bharti");
	  trie.Add("Hiral");
	  trie.Add("Nitin");
	  String str = "MilindKhadloya";
	  trie.Add(str);
	  //Search
	  Search("Milind",trie.Search("Milind"));
	   str = "Bharti";
	  Search(str,trie.Search(str),true);
	  str = "mil";
	  Search(str,trie.Search(str),false);
	  str = "nitink";
	  Search(str,trie.Search(str),false);
	  
	  str = "hiral";
	  Search(str,trie.Search(str),false);
	  
	  str = "milindkhadloya";
	  Search(str,trie.Search(str),false);
	  
	  str = "milindk";
	  Search(str,trie.Search(str),false);
	  
	  str = "MilindKhadloya";
	  Search(str,trie.Search(str),true);
	  
	  if (trie.Remove(str))
		  Console.WriteLine("removed string {0} from trie", str);
	  Search(str,trie.Search(str),false);
	  
	  Search("Milind",trie.Search("Milind"),true);
	  TestCase testcase = new TestCase();
	  testcase.SimpleTrie(trie);
	  
	}
}