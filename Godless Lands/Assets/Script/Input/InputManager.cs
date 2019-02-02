using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InpuPanelBar
{
    public class InputManager
    {
        private static readonly Token[] allowTokenForMaiPanel =
        {
            new Token(KeyCode.Alpha1, "1"),
            new Token(KeyCode.Alpha2, "2"),
            new Token(KeyCode.Alpha3, "3"),
            new Token(KeyCode.Alpha4, "4"),
            new Token(KeyCode.Alpha5, "5"),
            new Token(KeyCode.Alpha6, "6"),
            new Token(KeyCode.Alpha7, "7"),
            new Token(KeyCode.Alpha8, "8"),
            new Token(KeyCode.Alpha9, "9"),
            new Token(KeyCode.Alpha0, "0"),
            new Token(KeyCode.F1, "F1"),
             new Token(KeyCode.F2, "F2"),
              new Token(KeyCode.F3, "F3"),
               new Token(KeyCode.F4, "F4"),
                new Token(KeyCode.F5, "F5"),
                 new Token(KeyCode.F6, "F6"),
                  new Token(KeyCode.F7, "F7"),
                   new Token(KeyCode.F8, "F8"),
                    new Token(KeyCode.F9, "F9"),
                     new Token(KeyCode.F10, "F10"),
                      new Token(KeyCode.F11, "F11"),
                       new Token(KeyCode.F12, "F12"),
                        new Token(KeyCode.Q, "Q"),
                         new Token(KeyCode.E, "E"),
                          new Token(KeyCode.T, "T"),
                           new Token(KeyCode.Y, "Y"),
                            new Token(KeyCode.U, "U"),
                             new Token(KeyCode.O, "O"),
                              new Token(KeyCode.F, "F"),
                               new Token(KeyCode.G, "G"),
                                new Token(KeyCode.H, "H"),
                                 new Token(KeyCode.J, "J"),
                                  new Token(KeyCode.K, "K"),
                                   new Token(KeyCode.L, "L"),
                                    new Token(KeyCode.Z, "Z"),
                                     new Token(KeyCode.X, "X"),
                                      new Token(KeyCode.C, "C"),
                                       new Token(KeyCode.V, "V"),
                                        new Token(KeyCode.B, "B"),
                                         new Token(KeyCode.N, "N"),
                                          new Token(KeyCode.M, "M")
        };


        public static Token GetToken(int index)
        {
            
            return allowTokenForMaiPanel[index];
        }
    }
}