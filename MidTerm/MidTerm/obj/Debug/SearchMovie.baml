   M S B A M L   `   `   `  ????B  >MidTerm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MidTermMidTerm  5   	       MidTerm.SearchMovie   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:MidTerm     5   	     ? Title$  SearchMovie??5   	   .??5
      ]? -   5         ]?Name$ 
ParentGrid??5
      .+?5   
   ?? -   5         ??Name$ Movies??5   C   $	??Top=?6   
???????$	??200??6S   $	??200??6c   $??Stretch=?5      $
??Auto=?61    ? ContextMenu 5      ?? 5      .@?5      m? -   5        $??Add to Wish list??5      69   m? 5      -   5        $??Add to Owned list??5      6:   5       ?? View 5      ?? 5      .P?5      ?? $??Title??6%    ?? Width$ 90??64    ?? DisplayMemberBinding 6?   ?? *Name+?? 5      $
??Year??6%   $ 75??63    6>   ?? *Year+?? 5      $??Genre??6%   $ 90??64    6?   ?? *Genre+?? 5      $??	Publisher??6%   $ 90??68    6C   ?? *	Publisher+?? 5      $??Other Sales??6%   $ 75??6:    6E   ?? *Other_Sales+?? 5      $??Global Sales??6%   $ 75??6;    6F   ?? *Global_Sales+?? 5      $??Audience score %??6%   $ 75??6?    6J   ?? *AudienceScore+?? 5      $??Rotten Tomatoes %??6%   $ 90??6@    6K   ?? *RottenTomatoes+5      5      ?? 5!   
   $	??Top=?6   $??20q?6*   ???	#FF00FF1A.+?5,      ?? -   5         ??Name$ 	CBParent1??5,       ?? Orientation$ 
Horizontal=?6,   $??5q?6E   $??Center=?6P   .+?5-      ?? $??Title??6   $??5,5,0,5q?6(   ?? 5.      -   5        	 ??Name$	 	MovieName??5.      $	??100??6-   $??30??69   $??0,5,5,5q?6E   ?? 50      $??Genre??6   $??5,5,0,5q?6(   ?? 51      -   5        
 ??Name$
 
MovieGenre??51      $	??100??6/   $??30??6;   $??0,5,5,5q?6G   ?? 53      $??	Publisher??6   $??5,5,0,5q?6,   ?? 54      -   5        $
 MoviePublisher??54      $	??100??63   $??30??6?   $??0,5,5,5q?6K   ?? 56      $
??Year??6   $??5,5,0,5q?6'   ?? 57      -	   5        $
 	MovieYear??57      $	??100??6.   $??30??6:   $??0,5,5,5q?6F   58      ?? 5:      $ 
Horizontal=?6   .+?5;      ?? -
   5         ??Name$ SearchMovieButton??5;      $??Center=?64   $??Search??6Q   $??85??6b   $??30??6m   $??15q?6?   ?? 5<      -   5        $ ClearSearchButton??5<      $??Clear Search??64   $??85??6K   $??30??6V   $??331,15,331.286,15q?6w    J? RenderTransformOrigin$ 	0.5,0.505&?6?   5=      5>      5?      5@      