package main

import "fmt"
import "math"

type twoDarray [][]float64

type node struct {
	left, right *node
	value       float64
}

func main() {
	sample := twoDarray{
		[]float64{0},
		[]float64{0, 1},
		[]float64{1, 0},
		[]float64{2, 1, 3},
		[]float64{2, 3, 1},
		[]float64{8, 3, 1, 6, 4, 7, 10, 14, 13},
		[]float64{6, 4, 2, 1, 3, 5, 8, 7, 9, 10},
		[]float64{31, 12, 5, 21, 37, 35, 33, 36, 77, 52, 93, 90, 94},
		[]float64{31, 12, 5, 21, 37, 35, 33, 36, 77, 1, 52, 93, 90, 94},
		[]float64{31, 12, 5, 21, 37, 35, 33, 36, 77, 52, 93, 1, 90, 94},
		[]float64{31, 12, 5, 21, 1, 37, 35, 33, 36, 77, 52, 93, 1, 90, 94}}

	for i := 0; i < len(sample); i++ {
		fmt.Println("Testing: ", sample[i])
		bst := CreateBST(sample[i])
		if !IsBST(bst) {
			fmt.Println("Error, the resulting tree is not a binary search tree")
			continue
		}
		test := PreOrderTraversal(bst, len(sample[i]))
		if !Equal(test, sample[i]) {
			fmt.Println("Error, trees are not equal")
		} else {
			fmt.Println("Pass!")
		}
	}
}

func Equal(a, b []float64) bool {
	if len(a) != len(b) {
		return false
	}

	for i := range a {
		if a[i] != b[i] {
			return false
		}
	}

	return true
}

func IsBST(tree *node) bool {
	return isValidBST(tree, -math.MaxFloat64, math.MaxFloat64)
}

func isValidBST(tree *node, min, max float64) bool {
	if tree == nil {
		return true
	}
	if tree.value > min && tree.value < max && isValidBST(tree.left, min, math.Min(tree.value, max)) && isValidBST(tree.right, math.Min(tree.value, max), max) {
		return true
	} else {
		return false
	}
}

func CreateBST(preOrder []float64) *node {
	if len(preOrder) < 1 {
		return nil
	}

	root := &node{nil, nil, preOrder[0]}
	index := len(preOrder)
	for i := 0; i < len(preOrder); i++ {
		if preOrder[i] > preOrder[0] {
			index = i
			i = len(preOrder)
		}
	}

	root.left = CreateBST(preOrder[1:index])
	root.right = CreateBST(preOrder[index:len(preOrder)])

	return root
}

func PreOrderTraversal(tree *node, length int) []float64 {
	if tree == nil {
		return nil
	}
	var traversal = make([]float64, length)
	index := new(int)
	*index = 0
	Traverse(tree, traversal, index)
	return traversal
}

func Traverse(tree *node, traversal []float64, index *int) {
	if tree == nil {
		return
	}
	traversal[*index] = tree.value

	*index++
	Traverse(tree.left, traversal, index)
	Traverse(tree.right, traversal, index)
}
