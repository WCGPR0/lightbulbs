# Lightbulbs

Due to having to work on an old Mac laptop, this project is built targetting .NET Core 2.2.8, and using [Angular CLI](https://github.com/angular/angular-cli) version 8.3.19.

This is using SCSS as a stylesheet preprocessor, Angular, and ASP.NET Web Forms & .NET Core 2.2.


Favicon Icon made by [Freepik](https://www.flaticon.com/authors/freepik) from www.flaticon.com.

### Problem

There are 100 light bulbs lined up in a row in a long room. Each bulb has its own switch and is currently switched off. The room has an entry door and an exit door. There are 100 people lined up outside the entry door. Each bulb is numbered consecutively from 1 to 100. So is each person. Person No. 1 enters the room, switches on every bulb, and exits. Person No. 2 enters and flips the switch on every second bulb (turning off bulbs 2, 4, 6, ...). Person No. 3 enters and flips the switch on every third bulb (changing the state on bulbs 3, 6, 9, ...). This continues until all 100 people have passed through the room. How many of the light bulbs are illuminated after the 100th person has passed through the room? More specifically, which light bulbs are still illuminated, and why?

### Attempted Solution

The standard idea was just the brute force approach, in which you build up the light bulbs, m, and cycle through for each person, n. The big O would then be, O(m/1 + m/2 + m/3... + m/n), which becomes O(m H<sub>n</sub>), or approximately O(m ln(n)).

To improve performance, the results are being cached, so that when the input gets changed, it can continue from the closest entry point instead of having to start from scratch again.

The optimized idea (not yet implemented) is to use the properties of square numbers - in that, they have an odd number of unique factors. This would result in the big O(Sqrt(k)). However this only would be applicable under the constraints of when the number of lightbulbs, k, is the same as the number of people, k.

The experimental idea (not yet implemented) is to capitalize in the event that these calculations are independent, and hence could be done in parallel. IE. 1-10, 2-20, 3-30, etc. could be done in different threads, and the results are joined together using the XOR operator. Definitely not a purist approach, but this could be even more capitalized by treating the client as a thread, and having some of the work done by the client and the rest done by the server, and getting the results merged.