# Technical Test - Word Ladder
 - It's a technical test to solve A Word Ladder Puzzle

# The Problem

- Write a program to solve a word puzzle
- Choose a start word
- Choose an end word
- Come up with a list of words that move from the start word to the end word
- Only one letter can change between any two words
- Each intermediate step should be a word from the dictionary file (supplied) - words-english.txt

# Requeriments

- Should be a command line application
- Written in C#.Net (core or framework)
- It should use the command line to input these arguments
    - Start Word
    - End Word
    - Dictionary file (supplied) - words-english.txt
    - Answer file name

- Start and End word should be 4 characters long
- The solution should write the final list of words into a text file
- Include a single page write up
    - Document how you tackled the problem
    - References any articles/methodologies you used
- Return a link to the solution (or zip)

# How the Word Puzzle was Handled ? 

- Created a prompt so the user can input all the information needed
- Get the Start Word, End Word, Words Length, File Name of the Dictionary containing all the words (words-english.txt) and Result file Name to save the results in the end
- Run some validations on fields
- Filter the Words Dictionary based on Words Length provided
- Send all the informations to a Calculator that extracts the Result Words List
    - Extract all the words with one different letter and generate a new list with each (start word and one different letter word)
    - Save the first iteration with that different letter as a word step
    - Extract new one letter different words from the last word inside previous word step
    - Keep extracting words like this until find the end word inside the one different letter words list and that's your goal
    - You will be able to generate the shortest path from your start word until your end word with this logic
    - Check more details inside the source code on WordCalculator.cs
- Save the result list on the Result File Name provided
- Show the results on prompt with all the information needed

OBS: The solution accepts words with it's length lesser and higher than the value asked (4)

# Tecnhical - Principles and Patterns used on the solution:

- Dependency Injection and Inversion of Control which make the application more loosely coupled allowing reusability, testability and avoiding direct dependencies.
- Generics
- LINQ which is a more familiar language data source / format independent, less coding lines, standardized, IntelliSense support and etc.
- Used SOLID Principles.
- Used KISS Principle.
- Used NuGet for Unity and Unit tests xUnit.
- Used Threads(Parallel.ForEach) to speed up the loops execution so it may not be a problem if it scales up. Could have created async and await but didn't wanted to do it on this solution.
- Used Factory Method Pattern. Didn't feel the need to use others Design Patterns based on the simple solution and time i had to do it.
- All the Exceptions were handled and showed on prompt, so i didn't wanted to create a Logger for this solution, but that could've been done.
- Test Driven Development (Most part of the time) using (xUnit) to create Unit Test. Didn't feel the need to Mock anything using Moq.
- Could have created a project called "Domain" to put the Word.cs there, since it's not a anemic class and would make sense to be used as a Domain, but didn't want to due to the simple solution.
- It was Designed for Testability and Scalability (Could have more Datasources and Generics being used)
- Adherence to coding standards
- Performance Considerations ( Used Multithreads for the heaviest iterations)
- Reusability
- Used NuGet for Unity and xUnit
- Used GitHub for source control


# References:

- https://en.wikipedia.org/wiki/Word_ladder
- https://en.wikipedia.org/wiki/Collins_Scrabble_Words
- https://www.geeksforgeeks.org/word-ladder-length-of-shortest-chain-to-reach-a-target-word
- http://ceptimus.co.uk/?page_id=61