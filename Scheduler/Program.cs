using Client.Console;
using Client.Console.Receivers.Factories;
using Client.Console.Senders.Factories;
using Client.Core.Commands;
using Client.Core.Receivers;
using Client.Core.Senders;
using Scheduler.Core.Logic.Scheduler;
using Scheduler.Core.Logic.Scheduler.Factory;

internal class Program
{
    private static void Main(string[] args)
    {
        var clientFactory = new ConsoleClientFactory();
        var client = clientFactory.Create();
        
        var schedulerFactory = new SchedulerFactory();
        var scheduler = schedulerFactory.Create();

        var stringReceiverFactory = new StringReceiverFactory();
        var stringReceiver = stringReceiverFactory.Create();

        var scheduleReceiverFactory = new ScheduleReceiverFactory(stringReceiverFactory, scheduler);
        var scheduleReceiver = scheduleReceiverFactory.Create();

        var scheduleTaskReceiverFactory = new ScheduleTaskReceiverFactory(stringReceiverFactory, scheduler);
        var scheduleTaskReceiver = scheduleTaskReceiverFactory.Create();

        var scheduleSenderFactory = new ScheduleSenderFactory();
        var scheduleSender = scheduleSenderFactory.Create();

        var scheduleTaskSenderFactory = new ScheduleTaskSenderFactory();
        var scheduleTaskSender = scheduleTaskSenderFactory.Create();

        var commands = CreateCommands(scheduler, stringReceiver, scheduleReceiver, scheduleTaskReceiver, scheduleSender, scheduleTaskSender);

        client.Start(commands);
    }

    private static ICollection<ICommand> CreateCommands(IScheduler scheduler, IStringReceiver stringReceiver, IScheduleReceiver scheduleReceiver, IScheduleTaskReceiver scheduleTaskReceiver, IScheduleSender scheduleSender, IScheduleTaskSender scheduleTaskSender)
    {
        var commands = new List<ICommand>();

        var addScheduleCommand = new AddScheduleCommand(scheduler, stringReceiver);
        commands.Add(addScheduleCommand);
        var addScheduleTaskCommand = new AddScheduleTaskCommand(scheduler, scheduleReceiver, stringReceiver);
        commands.Add(addScheduleTaskCommand);
        var getSchedulesCommand = new GetSchedulesCommand(scheduler, scheduleSender);
        commands.Add(getSchedulesCommand);
        var getScheduleTasksCommand = new GetScheduleTasksCommand(scheduler, scheduleReceiver, scheduleTaskSender);
        commands.Add(getScheduleTasksCommand);
        var removeScheduleCommand = new RemoveScheduleCommand(scheduler, scheduleReceiver);
        commands.Add(removeScheduleCommand);
        var removeScheduleTaskCommand = new RemoveScheduleTaskCommand(scheduler, scheduleReceiver, scheduleTaskReceiver);
        commands.Add(removeScheduleTaskCommand);

        return commands;
    }
}