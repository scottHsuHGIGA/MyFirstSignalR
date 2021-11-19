using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyFirstSignalR.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
            Clients.All.Test("隨便");
            JoinRoom("TestRoom");
            Clients.Group("TestRoom").addChatMessage(name, "隨便");
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).addChatMessage("system",Context.User.Identity.Name + " joined.");
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}