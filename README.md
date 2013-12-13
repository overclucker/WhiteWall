WhiteWall
=========

WhiteWall is intended to be an hassle free solution for blocking repeated attacks on servers using BattlEye.

Version: v1.0.0

Installation:

1:  Edit the settings in WhiteWall.exe.config to your needs.

2:  source whitewall.sql into your database.

3: That's it!


Usage:

WhiteWall is designed to be an easy to use whitelisting tool. No more manually adding players to the database; WhiteWall adds any player who connects to the database when it is set in OFF mode. Switch it to ON mode to block any player not currently in the database. You can change the mode that WhiteWall starts in by changing the value of "mode" in "WhiteWall.exe.config" between "on" and "off".