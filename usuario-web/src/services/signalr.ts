import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5036/usuariohub")
  .withAutomaticReconnect()
  .build();

export default connection;
