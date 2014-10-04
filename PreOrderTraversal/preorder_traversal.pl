#!/usr/bin/perl
use strict;

## Sample Data
my $sample = [
               [ 0 ],
               [ 0, 1 ],
               [ 1, 0 ],
               [ 2, 1, 3 ],
               [ 2, 3, 1 ],
               [ 8, 3, 1, 6, 4, 7, 10, 14, 13 ], 
               [ 6, 4, 2, 1, 3, 5, 8, 7, 9, 10 ],
               [ 31, 12, 5, 21, 37, 35, 33, 36, 77, 52, 93, 90, 94 ],
            ];

## Walk the test cases 
foreach my $test (@{$sample}){
  my $test_result = [];
  preorder_traversal(create_BST($test),$test_result);
  print "Testing: " . join(", ", @{$test}, "\t");
  if(@{$test} ~~ @{$test_result}){
    print "Passed\n";
  }else{
    print "Error, trees are not equal\n";
    print "Got:     " . join(", ", @{$test_result}, "\n");
  }
}

## walk the values to get inserted into tree
sub create_BST{
  my $list = shift;
  my $tree;
  foreach my $value (@$list){
    insert($tree,$value);
  }  
  return $tree;
}

## build the tree
sub insert{
  my ($tree, $value) = @_;
  if(!$tree){
    $tree->{value} = $value;
    $tree->{right} = undef;
    $tree->{left} = undef;
    $_[0] = $tree;
    return;
  }
  if($tree->{value} > $value) {
    insert($tree->{left},$value);
  }elsif($tree->{value} < $value){
    insert($tree->{right},$value);
  }
}

sub preorder_traversal{
  my $node = shift;
  my $traversal = shift;
  if (!$node) { 
    return;
  }
  push @$traversal, $node->{value};
  preorder_traversal($node->{left},$traversal);
  preorder_traversal($node->{right},$traversal);
}
