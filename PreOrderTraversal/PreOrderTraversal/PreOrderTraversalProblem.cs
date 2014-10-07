﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreOrderTraversal
{
    public class PreOrderTraversalProblem
    {
        /// <summary>
        /// Simplest implementation of a Binary Tree
        /// </summary>
        public class Node
        {
            #region " Properties "
            /// <summary>
            /// The left child of the current node
            /// </summary>
            public Node Left
            {
                get;
                set;
            }

            /// <summary>
            /// The right child of the current node
            /// </summary>
            public Node Right
            {
                get;
                set;
            }

            /// <summary>
            /// The value of the current node
            /// </summary>
            public int Value
            {
                get;
                set;
            }
            #endregion " Properties "

            #region " Constructors and Public Methods "
            /// <summary>
            /// The constructor initializes the value of the node
            /// </summary>
            /// <param name="value">The value with which to initialize this node</param>
            public Node(int value)
            {
                Value = value;
            }
            #endregion " Constructors and Public Methods "
        }

        /// <summary>
        /// This function creates a binary search tree from a pre-order traversal of a binary search tree
        /// </summary>
        /// <param name="preOrderTraversal">An array of integers representing the pre-order traversal</param>
        /// <returns>A binary search tree created from the input</returns>
        public static Node CreateBST(int[] preOrderTraversal)
        {
            if (preOrderTraversal.Length < 1) return null;
            int start = 0;
            int end = preOrderTraversal.Length;
            Node root = new Node(preOrderTraversal[start]);

            int index = end;
            for (int i = start; i < end; i++)
            {
                if (preOrderTraversal[i] > preOrderTraversal[start])
                {
                    index = i;
                    break;
                }
            }
            for (int i = index; i < end; i++)
            {
                if (preOrderTraversal[i] < preOrderTraversal[start])
                {
                    return null;
                }
            }

            int[] leftTraversal = new int[index - start - 1];
            Array.Copy(preOrderTraversal, start + 1, leftTraversal, 0, leftTraversal.Length);
            root.Left = CreateBST(leftTraversal);

            int[] rightTraversal = new int[end - index];
            Array.Copy(preOrderTraversal, index, rightTraversal, 0, rightTraversal.Length);
            root.Right = CreateBST(rightTraversal);

            return root;
        }

        /// <summary>
        /// This function does a pre-order traversal of a binary search tree
        /// </summary>
        /// <param name="node">The binary search tree to travers</param>
        /// <param name="length">The number of nodes within the tree</param>
        /// <returns>An array that represents the pre-order traversal of the tree</returns>
        public static int[] PreOrderTraversal(Node node, int length)
        {
            if (node == null)
            {
                return null;
            }
            int[] traversal = new int[length];
            int index = 0;
            PreOrderTraversal(node, traversal, ref index);
            return traversal;
        }

        /// <summary>
        /// This function does a pre-order traversal of a binary search tree
        /// </summary>
        /// <param name="node">The binary search tree to traverse</param>
        /// <param name="traversal">The array that will hold the pre-order traversal of the tree</param>
        /// <param name="index">The index of the array to fill</param>
        private static void PreOrderTraversal(Node node, int[] traversal, ref int index)
        {
            if (node == null)
            {
                return;
            }
            traversal[index] = node.Value;
            index++;
            PreOrderTraversal(node.Left, traversal, ref index);
            PreOrderTraversal(node.Right, traversal, ref index);
        }

        static void Main(string[] args)
        {
            // Test Cases
            int[][] sample = new int[][]{
                new int[]{ 0 },
                new int[]{ 0, 1 },
                new int[]{ 1, 0 },
                new int[]{ 2, 1, 3 },
                new int[]{ 2, 3, 1 },
                new int[]{ 8, 3, 1, 6, 4, 7, 10, 14, 13 }, 
                new int[]{ 6, 4, 2, 1, 3, 5, 8, 7, 9, 10 },
                new int[]{ 31, 12, 5, 21, 37, 35, 33, 36, 77, 52, 93, 90, 94 }
            };
            
            int[] test;

            for ( int i = 0; i < sample.Length; i++)
            {
                Console.WriteLine("Testing: " + string.Join(", ", sample[i]));
                
                // Get the pre-order traversal of the tree that's generated by the CreateBST function
                test = PreOrderTraversal(CreateBST(sample[i]), sample[i].Length);

                // Compare the test array with the sample. If they are equal, then the function produced
                // the correct binary search trees
                if (test == null || !sample[i].SequenceEqual(test))
                {
                    Console.WriteLine("Error, trees are not equal");
                }
                else
                {
                    Console.WriteLine("Pass!");
                }
            }
            Console.ReadLine();
        }
    }
}