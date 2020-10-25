# Toy Robot Challenge

## Description

The application is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 units. There are no other obstructions on the table surface.

### Constraints

The toy robot is free to roam around the surface of the table, but must be prevented from falling to destruction. Any movement that would result in the robot falling from the table must be prevented, however further valid movement commands must still be allowed.

The toy robot listens to the following commands:

* **PLACE X,Y,F**         - *Places the robot to on X,Y coordinates facing NORTH,SOUTH,EAST or WEST.*
* **MOVE**                - *Moves 1 unit in facing direction*
* **LEFT**                - *Rotates the robot 90 degrees to left in the specified direction without changing the position of the robot*
* **RIGHT**               - *Rotates the robot 90 degrees to right in the specified direction without changing the position of the robot*
* **REPORT**              - *Reports current position and facing direction*

## Environments
This console application was built on Windows 10 using .net core 3.1. It should run without a problem on Windows system, and probably on other system too is because .net core across different Platform like Windows, macOS or Linux.

## Installation

- Dotnet Core SDK (latest 3.1)
   - [Download for windows x64](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.403-windows-x64-installer), [macOS](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.403-macos-x64-installer), [Linux](https://docs.microsoft.com/dotnet/core/install/linux-package-managers)
   - After downloaded and installed it, please open teminal and try this command as below to check if it is installed on your local
    ```
    dotnet --version
    ```

- Visual Studio Community 2019
  - [Download](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16)

- Toy Robot Challenge global tools (optional)
  -  Running this commad as below on CLI, after that, you installed global tools for toy robot on local (must install Dotnet Core SDK before running it)
    ```
    dotnet tool install --global toyrobot --version 1.0.2
    ```

## Usage
- cd to the **ToyRobotChallenge** directory and run the console application with:
  
  ``` 
  dotnet run 
  ```

- You can also run the unit test suite with:

  if you cd to the **ToyRobotChallenge.Tests** directory, then run,
  ``` 
  dotnet test 
  ``` 
  
  different system or CLI may use other syntax to find ToyRobotChallenge.Tests directory
  
  ``` 
  dotnet test ../ToyRobotChallenge.Tests 
  ``` 

## Architecture Details && Thoughts

The project using interfaces and polymorphism concepts as project architecture to flexibly implement toy robot challenge features like table, robot, command and simulator. The benifits are code robustness, extensibility and readability. For example, if we need to add a new Robot command calling <span style="background-color:grey">**Back**</span> which is go back 1 unit, the opposite of Move command. So some sample steps as below,
- You can easily create a new class calling BackCommand and inherit from interface, then implement relevant functions which from interface.
- Add new command type calling BACK in the enumeration.
- Add new BackCommand as a polymorphic behavior of interfaces in the Command Factory.
- Lastly, add one robot action in the IRobot and implement it in the ToyRobot class.

Also, we using the whole **Command** pattern invites a Factory pattern for command feature. The advantage of doing this is that our **CommandHandler** class doesn't have the responsibility of knowing all the possible commands and the commands can be easily tested/mocked in the future.

## Global Tools (optional)
Please follow how to install global tool for toy robot challenge as above, using .NET CORE Global tools as a SDK, it can quickly running this console application , even if you neither pull the source code nor install your IDE, the command as below,

``` 
toyrobot -help 
toyrobot -interative
toyrobot [Test file path]
```

## More docs 
- In the **Docs** directory under project root

## TBD
- New robot command feature (like **Back**, **Undo**, **Reset**, maybe **Jump**)
- UI/UX implementation