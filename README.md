# ChampionsLeagueAPI

# The different endpoint user can use:
GET championsleagueapi/club
GET championsleagueapi/club/{id}
DELETE championsleagueapi/club/{id}
POST championsleagueapi/club
GET championsleagueapi/player
GET championsleagueapi/player/{id}
DELETE championsleagueapi/player/{id}
POST championsleagueapi/player

# Sample request bodies:
For club
{
    "clubName": "FC Barcelona",
    "coachName": "Xavi Hernandez",
    "location": "Barcelona, Spain",
    "teamStatus": "Out"
}

For player
{
    "firstName": "Ederson",
    "lastName": "Moraes",
    "jerseyNumber": 31,
    "position": "GK",
    "goals": 0,
    "clubId": 3
}

# Sample response body:
From GET championsleagueapi/club request
{
    "statusCode": 200,
    "statusDescription": "Clubs found",
    "items": [
        {
            "clubId": 1,
            "clubName": "FC Barcelona",
            "coachName": "Xavi Hernandez",
            "location": "Barcelona, Spain",
            "teamStatus": "Out"
        }
    ]
}

# Changes:
I only made 1 change from my API idea presentation and that is to use "DELETE" HTTP method instead of "PUT". The rest are the same, primary key, foreign key, table, as well as additional constraint that is not used as primary key or a foreign key (in my case its not null and its used on almost all the column).
