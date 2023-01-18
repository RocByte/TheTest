// See https://aka.ms/new-console-template for more information

using BattleshipsGameConsole;

var targetPracticeMatch = ConsoleMatchFactory.GetTargetPracticeMatch(
    squareGridSize: 10,
    initialShipSizes: new[] { 5, 4, 4 });

targetPracticeMatch.Start();


