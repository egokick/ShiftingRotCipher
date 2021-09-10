﻿using System;
using System.Collections.Generic;
using static ShiftingRotationCipher.CipherAction;

namespace ShiftingRotationCipher
{
    public class UserInput
    {
        public string TextInput { get; set; }
        public string TextOutput { get; set; }
        public string ShiftingRotationChiper { get; set; }
        public CipherAction CipherAction { get; set; }

        public UserInput(CipherAction cipherAction)
        {
            CipherAction = cipherAction;
        }
    }

    public enum CipherAction
    {
        Encode,
        Decode
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 = Encode Text");
                Console.WriteLine("2 = Decode Text");
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        var encodeUserInput = EncodeTextGetInputs();
                        encodeUserInput = OffsetText(encodeUserInput);
                        ShowEncodedText(encodeUserInput);
                        Console.ReadLine();
                        break;
                    case "2":
                        var decodeUserInput = new UserInput(Decode);
                        Console.WriteLine("Enter text to decode:");
                        decodeUserInput.TextInput = Console.ReadLine();
                        Console.WriteLine("Enter Shifting Rotation Cipher:");
                        decodeUserInput.ShiftingRotationChiper = Console.ReadLine();
                        decodeUserInput = OffsetText(decodeUserInput);
                        Console.WriteLine();
                        Console.WriteLine("Decoded text:");
                        Console.WriteLine(decodeUserInput.TextOutput);
                        break;
                    default:
                        Console.WriteLine("Input not recognized");
                        break;
                }
            }
        }

        public static UserInput OffsetText(UserInput userInput)
        {
            var textLength = userInput.TextInput.Length;
            var cipherLength = userInput.ShiftingRotationChiper.Length;

            var text = userInput.TextInput.ToCharArray();
            var mod = 0;
            var offset = 0;
            for (var i = 0; i < textLength; i++)
            {
                if (text[i] == ' ') continue; // don't encode spaces
                if (i != 0) mod = i % cipherLength; // loop the cipher if shorter than text to encode

                offset = userInput.CipherAction switch
                {
                    Encode => (NumberWang[text[i]] + NumberWang[userInput.ShiftingRotationChiper[mod]]) % 26,
                    Decode => (NumberWang[text[i]] - NumberWang[userInput.ShiftingRotationChiper[mod]]) % 26,
                    _ => offset
                };
                text[i] = WangNumber[offset];
            }
            userInput.TextOutput = string.Join("", text);
            return userInput;
        }
   

        public static void ShowEncodedText(UserInput encodeUserInput)
        {
            Console.WriteLine();
            Console.WriteLine("Encoded Text:");
            Console.WriteLine(encodeUserInput.TextOutput);
        }
         
        public static UserInput EncodeTextGetInputs()
        {
            var encodeUserInput = new UserInput(Encode);
            Console.WriteLine("Enter text to encode:");
            encodeUserInput.TextInput = Console.ReadLine().ToLower();
            Console.WriteLine("Enter Shifting Rotation Cipher:");
            encodeUserInput.ShiftingRotationChiper = Console.ReadLine();
            return encodeUserInput;
        }

        public static Dictionary<char, int> NumberWang = new Dictionary<char, int>()
        {
            {' ', 0}, // No changes to space character
            {'1', 1},
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'0', 0},
            {'a', 1},
            {'b', 2},
            {'c', 3},
            {'d', 4},
            {'e', 5},
            {'f', 6},
            {'g', 7},
            {'h', 8},
            {'i', 9},
            {'j', 10},
            {'k', 11},
            {'l', 12},
            {'m', 13},
            {'n', 14},
            {'o', 15},
            {'p', 16},
            {'q', 17},
            {'r', 18},
            {'s', 19},
            {'t', 20},
            {'u', 21},
            {'v', 22},
            {'w', 23},
            {'x', 24},
            {'y', 25},
            {'z', 26}
        };

        public static Dictionary<int, char> WangNumber = new Dictionary<int, char>()
        {
            {0, ' '},  // No changes to space character
            {1, 'a'},
            {2, 'b'},
            {3, 'c'},
            {4, 'd'},
            {5, 'e'},
            {6, 'f'},
            {7, 'g'},
            {8, 'h'},
            {9, 'i'},
            {10, 'j'},
            {11, 'k'},
            {12, 'l'},
            {13, 'm'},
            {14, 'n'},
            {15, 'o'},
            {16, 'p'},
            {17, 'q'},
            {18, 'r'},
            {19, 's'},
            {20, 't'},
            {21, 'u'},
            {22, 'v'},
            {23, 'w'},
            {24, 'x'},
            {25, 'y'},
            {26, 'z'}
        };

    }
}