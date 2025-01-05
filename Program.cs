using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;

namespace Socket_Detective
{
    internal class Program
    {
        public List<int> recomend = new List<int>
        {
            20,    // FTP (Data)
            21,    // FTP (Control)
            22,    // SSH
            23,    // Telnet
            25,    // SMTP
            53,    // DNS
            80,    // HTTP
            110,   // POP3
            143,   // IMAP
            443,   // HTTPS
            3389,  // RDP
            465,   // SMTP (Secure)
            993,   // IMAP (Secure)
            995    // POP3 (Secure)
        };
        public List<int> Full = new List<int>
        {
                20,    // FTP (Data)
    21,    // FTP (Control)
    22,    // SSH
    23,    // Telnet
    25,    // SMTP
    53,    // DNS (TCP)
    67,    // DHCP (Server)
    68,    // DHCP (Client)
    69,    // TFTP
    80,    // HTTP
    110,   // POP3
    123,   // NTP
    135,   // RPC
    137,   // NetBIOS Name Service
    138,   // NetBIOS Datagram Service
    139,   // NetBIOS Session Service
    143,   // IMAP
    443,   // HTTPS
    465,   // SMTP (Secure)
    514,   // Syslog
    520,   // RIP
    554,   // RTSP
    563,   // NNTP (Secure)
    993,   // IMAP over SSL
    995,   // POP3 over SSL
    1080,  // SOCKS Proxy
    1433,  // MSSQL
    1521,  // Oracle Database
    161,   // SNMP
    162,   // SNMP Trap
    179,   // BGP
    389,   // LDAP
    445,   // SMB
    873,   // RSYNC
    2049,  // NFS
    3268,  // Global Catalog
    3306,  // MySQL
    3389,  // RDP
    5432,  // PostgreSQL
    5900,  // VNC
    6379,  // Redis
    8080,  // HTTP Alternative
    8081,  // HTTP Alternative
    8443,  // HTTPS Alternative
    9200,  // Elasticsearch
    1812,  // RADIUS Authentication
    1813,  // RADIUS Accounting
    27017, // MongoDB
    11211, // Memcached
    1883,  // MQTT
    8883,  // MQTT Secure
    500,   // IPSec/IKE
    514,   // Syslog
    161,   // SNMP
    2427,  // SIP
    5060,  // SIP (UDP)
    5061,  // SIP (Secure)
    5222,  // XMPP (Jabber) (Client connection)
    5269,  // XMPP (Jabber) (Server connection)
    25,    // SMTP
    993,   // IMAPS
    1194,  // OpenVPN
    1701,  // L2TP
    3389,  // RDP
    22,    // SSH
    443,   // HTTPS
    2049,  // NFS
    520,   // RIP
    2427,  // SIP
    500,   // ISAKMP (for VPN)
    5060,  // SIP (Voice Over IP)
    1194,  // OpenVPN
    1701,  // L2TP (Layer 2 Tunnel Protocol)
    1812,  // RADIUS Authentication
    1813,  // RADIUS Accounting
    520,   // RIP
    524,   // NCP
    25,    // SMTP
    23,    // Telnet
    31337  // Back Orifice (used in exploits)
        };
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome To port Detective for see help type help and perss enter ");
            IPAddress iP;
            string select;
            while (true)
            {
                select = Console.ReadLine();
                if (select != null)
                {
                    if (select == "help")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(File.ReadAllText("help.txt"));
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    else
                    {
                        List<string> argomants = new List<string>();
                        foreach (string value in select.Split(' '))
                        {
                            argomants.Add(value);
                        }
                        if (IPAddress.TryParse(argomants[0], out iP) && (argomants[1] == "-L" || argomants[1] == "-P"))
                        {
                            int min;
                            int max;
                            if (argomants[1] == "-L" && (argomants[2] == "TCP-full" || argomants[2] == "TCP-recomend"))
                            {
                                Program program = new Program();
                                switch (argomants[2])
                                {
                                    case "TCP-full":
                                        await program.scaner(program.Full, iP);
                                        continue;
                                    case "TCP-recomend":
                                        await program.scaner(program.recomend, iP);
                                        continue;
                                }
                            }
                            else if (argomants[1] == "-P" && int.TryParse(argomants[2], out min) && int.TryParse(argomants[3], out max))
                            {
                                Program program = new Program();
                                if (min < max)
                                {
                                    await program.coustom(iP, min, max);
                                }
                                else
                                {
                                    Console.WriteLine("wrong port number and chaeck min and max port");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("wrong commend chack and try again");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("type Corect for know how work see help with help commend ");
                            continue;
                        }
                    }
                }
            }

        }
        async Task coustom(IPAddress iP, int min, int max)
        {
            TcpClient client = new TcpClient();
            for (int i = min; i <= max; i++)
            {
                try
                {
                    client.Connect(iP, i);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(i + "port is open ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(i + "port is closed ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine("finish");
        }

        async Task scaner(List<int> ports, IPAddress ip)
        {
            for (int i = 0; i < ports.Count; i++)
            {
                TcpClient client = new TcpClient();
                try
                {
                    client.Connect(ip, ports[i]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(ports[i] + "port is open ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ports[i] + "is closed");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine("finish");
        }
    }
}
