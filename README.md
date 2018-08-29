# AirMVVM

## AirMVVM.Commands
This library includes some useful mvvm commands.

### SingleExecutionCommand
This command will executed once.

```
// ViewModel part
viewModel.SaveCommand = new SingleExecutionCommand(async () =>
                                        {
                                            await Save();
                                        });
BindingContext = viewModel;

// View
<Button Text="SaveText" Command="{Binding SaveCommand}" />

// Behaviour
After click button will be disabled. Command will executed once.
```

### SequentialСommand
This command may be invoked multiple times. But at one time, the command can be executed once. SequentialСommand may be useful for prevent double click problems.

```
// ViewModel part
viewModel.SaveCommand = new SingleExecutionCommand(async (commandFinishedCallback) =>
                                        {
                                            await Save();
                                            commandFinishedCallback();
                                        });
BindingContext = viewModel;

// View
<Button Text="SaveText" Command="{Binding SaveCommand}" />

// Behaviour
After click button will be disabled. After command execution button will be enabled again.
```