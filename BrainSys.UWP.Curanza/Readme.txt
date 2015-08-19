Welcome to Curanza
******************************************************
Curanza is a lightweight library useful to create powerful Universal Windows Platform (UWP) applications for Windows 10.
Using Curanza you can successfully apply MVVM pattern to your applications.

Curanza : Objective
******************************************************
The main purpose of Curanza is to simplify the development of a UWP app for any supported platform.
Today a modern app is developed using MVVM pattern, and Curanza helps you to successfully apply this pattern.
Using Curanza, for example, you can bind a RelayCommand (and optionally his parameter) to most typical gesture/events
performed by the user on the UI.

Curanza : Purposes
******************************************************
Curanza is built on top of MVVM Light Toolkit developed by Laurent Bugnion.
You can get more information about MVVM Light Toolkit at the following url:
https://mvvmlight.codeplex.com/
http://www.mvvmlight.net/
When you get Curanza from NuGet, you automatically download MVVM Light Toolkit. No worries about that!

Curanza : Scenarios
******************************************************
Using Curanza you can execute a command:
- when a TextBox matches a specified regular expression
- when a FrameworkElement on a page is loaded and rendered on the screen

Using Curanza you can execute a command when the user:
- press a specified key when a FrameworkElement has focus
- swipe over any UIElement (up, down, left and right)
- use the mouse wheel over any UIElement (up and down)
- make a Hold gesture over any UIElement
- make a double-tap gesture over any UIElement
- perform a drag'n'drop operation

Other typical usage:
- you can auto-select-all the content of a TextBox when it gets focus

Curanza : TextBox
You can bind a RelayCommand when a TextBox content matches a specified regular expression. Optionally, you can pass an object parameter.
In the example, the command RegexCommand is executed when the user insert a digit.
<TextBox Text="insert a digit to execute the command" curanza:TextBoxHelper.RegexCommand="{Binding RegexCommand}"
  curanza:TextBoxHelper.RegexExpression="[0-9]" curanza:TextBoxHelper.RegexCommandParameter="parameter 1"/>

In the example, the command RegexCommand is executed when the user insert three upper-case letters.
<TextBox Text="insert three upper case letters to execute the command" curanza:TextBoxHelper.RegexCommand="{Binding RegexCommand}"
  curanza:TextBoxHelper.RegexExpression="[A-Z][A-Z][A-Z]" curanza:TextBoxHelper.RegexCommandParameter="parameter 2"/>

Curanza : Mouse wheel
You can bind a RelayCommand when the user use the mouse wheel over any UIElement of the UI.
In the example, the commands DownCommand and UpCommand are executed when the user use the mouse.
Two different integer parameters are passed.
<Border Background="Red" Margin="32"
  curanza:Wheel.DownCommand="{Binding DownCommand}" curanza:Wheel.DownCommandParameter="5"
  curanza:Wheel.UpCommand="{Binding UpCommand}" curanza:Wheel.UpCommandParameter="20" />

Curanza : Swipe
You can bind a RelayCommand when the user swipes in the four directions over any UIElement.
In the example, four different commands are executed when the user swipes.
Four different string parameters are passed for each command.
<Border ManipulationMode="All"
  curanza:Swipe.DownCommand="{Binding DownCommand}" curanza:Swipe.DownCommandParameter="down parameter"
  curanza:Swipe.UpCommand="{Binding UpCommand}" curanza:Swipe.UpCommandParameter="up parameter"
  curanza:Swipe.LeftCommand="{Binding LeftCommand}" curanza:Swipe.LeftCommandParameter="left parameter"
  curanza:Swipe.RightCommand="{Binding RightCommand}" curanza:Swipe.RightCommandParameter="right parameter" />

Curanza : Hold
You can bind a RelayCommand when the user performs an hold gesture over any UIElement.
Optionally you can pass any parameter to the command.
See the example below.
<Border curanza:Hold.Command="{Binding Command}" curanza:Hold.CommandParameter="1" />

Curanza : ListView
You can bind a RelayCommand when the user clicks on a element inside a ListView. Internally the RelayCommand is executed when the ListView raises the ItemClick event.
You cannot pass a custom parameter to the command, because the clicked item is automatically used and sent to the RelayCommand.
In this example, the command GetDetailCommand is executed when the user clicks on a element; the clicked item is passed to the command.
<ListView ItemsSource="{Binding Items}"
  curanza:ListViewBaseHelper.ItemClickCommand="{Binding GetDetailCommand}">

Curanza - About
******************************************************
Curanza is developed by Brain-Sys Srl (2015)
http://www.brain-sys.it