using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stage1_Manager : Entity {

    const int MAX_ROW = 8;
    const int MAX_COLUMN = 5;
    //const int BOARD_LAYER = 2;
    //const int CARD_BOARD_LAYER = 2;
    const int MAX_PLAYER = 2;
    const int MAX_ENEMY = 2;

    //public int enemyTurn = 0;

    public int[] position = new int[2] { 0, 0 };

    //public string[,,] board = new string[MAX_HEIGHT, MAX_WIDTH, BOARD_LAYER];
    //public Card[,,] cardBoard = new Card[MAX_HEIGHT, MAX_WIDTH, CARD_BOARD_LAYER];
    public Board board = new Board(MAX_ROW, MAX_COLUMN);

    public GameObject[] character = new GameObject[MAX_PLAYER + MAX_ENEMY];

    //private bool playerTurn = true;

    //public string[] liveList = new string[MAX_PLAYER + MAX_ENEMY];

    private string turn;

    private Character[] c = new Character[MAX_PLAYER + MAX_ENEMY];
    private Player[] p = new Player[MAX_PLAYER];
    private Enemy[] e = new Enemy[MAX_ENEMY];

    void Start () {

        for (int i = 0, j = 0, k = 0; i < MAX_PLAYER + MAX_ENEMY; i++) {
            c[i] = character[i].GetComponent<Character>();
            if (c[i].type == "player") {
                p[j] = (Player)c[i];
                p[j].status = "alive";
                board.SetCharacterLabel(p[j].position[0], p[j].position[1], p[j].label);
                j++;
            }
            else {
                e[k] = (Enemy)c[i];
                e[k].status = "alive";
                board.SetCharacterLabel(e[k].position[0], e[k].position[1], e[k].label);
                k++;
            }
        }

        turn = "player";
        foreach (Player player in p) {
            player.isTurn = true;
        }
        //c[0] = character[0].GetComponent<Player>();
        //c[1] = character[1].GetComponent<Player>();
        //c[2] = character[2].GetComponent<Enemy>();
        //c[3] = character[3].GetComponent<Enemy>();
        /*for (int i = 0; i < MAX_PLAYER; i++) {
            p[i] = character[i].GetComponent<Player>();
            e[i] = character[i + MAX_PLAYER].GetComponent<Enemy>();
            liveList[i] = p[i].label;
            liveList[i + MAX_PLAYER] = "e" + e[i].e_tag.ToString();
        }*/

        /*for (int i = 0; i < MAX_HEIGHT; i++) {
            for (int j = 0; j < MAX_WIDTH; j++) {
                for (int k = 0; k < BOARD_LAYER; k++) {
                    board[i, j, k] = "n";
                }
                for (int k = 0; k < CARD_BOARD_LAYER; k++) {
                    cardBoard[i, j, k] = new Card(); 
                }
            }
        }

        board[p[0].position[0], p[0].position[1], 0] = "p" + p[0].p_tag.ToString();
        board[p[1].position[0], p[1].position[1], 0] = "p" + p[1].p_tag.ToString();
        board[e[0].position[0], e[0].position[1], 0] = "e" + e[0].e_tag.ToString();
        board[e[1].position[0], e[1].position[1], 0] = "e" + e[1].e_tag.ToString();*/

        /*for (int i = 0; i < MAX_PLAYER; i++) {
            board.SetCharacterLabel(p[i].position[0], p[i].position[1], p[i].label);
        }
        for (int i = 0; i < MAX_ENEMY; i++) {
            board.SetCharacterLabel(e[i].position[0], e[i].position[1], e[i].label);
        }*/

    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            position[0]--;
            position[0] = Mathf.Clamp(position[0], 0, MAX_ROW - 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            position[0]++;
            position[0] = Mathf.Clamp(position[0], 0, MAX_ROW - 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            position[1]--;
            position[1] = Mathf.Clamp(position[1], 0, MAX_COLUMN - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            position[1]++;
            position[1] = Mathf.Clamp(position[1], 0, MAX_COLUMN - 1);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            displaySelectPosition(position);
            //print(position[0].ToString() + position[1].ToString());
            displayBoard(board);
            //DisplayBoard(board, 0);
            //DisplayCardBoard(cardBoard, 1);
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            foreach(Player player in p) {
                player.HP = 0;
                DeathEvent(player);
            }
        }

        //TurnController();

        //can write here
        //end turn in class Stage1_Manager
        if (turn == "player" && Input.GetKeyDown(KeyCode.C)) {
            foreach (Player player in p) {
                print("5");
                player.isActing = false;
                player.step = 0;
                for (int i = 0; i < 3; i++) {
                    if (player.hand[i].ID == 0) {
                        player.hand[i] = player.deck.Draw();
                        if (player.hand[i].ID == 0) {
                            player.ResetDeck();
                            player.hand[i] = player.deck.Draw();
                        }
                    }
                }
            }
            EndTurn("player");
        }

    }

    public void activatePlayer (string label) {
        foreach (Player player in p) {
            if (player.label == label) {
                player.isActing = true;
            }
            else {
                if (player.step == 2) {
                    player.step = 1;
                }
                player.isActing = false;
            }
        }
    }

    /*public void TurnController () {
        if (playerTurn) {
            foreach (Player player in p) {
                player.isTurn = true;
            }
            foreach (Enemy enemy in e) {
                enemy.isTurn = false;
            }
        }
        else {
            foreach (Player player in p) {
                player.isTurn = false;
            }
            foreach (Enemy enemy in e) {
                enemy.isTurn = true;
            }
        }
    }*/

    public void enemyController (string label, string status) {
        switch (status) {
            case "start":
                foreach (Enemy enemy in e) {
                    if (enemy.label == label) {
                        enemy.isActing = true;
                        break;
                    }
                }
                break;
            case "end":
                for (int i = 0; i <= e.Length; i++) {
                    if (e[i].label == label  && i != e.Length - 1) {
                        e[i + 1].isActing = true;
                        break;
                    }
                    else if (e[i].label == label && i == e.Length - 1) {
                        EndTurn("enemy");
                        break;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void EndTurn (string type) {
        foreach (Character character in c) {
            if (character.type == type) {
                character.isTurn = false;
            }
            else {
                character.isTurn = true;
                if (type == "player") {
                    /*for (int i = 0; i <= e.Length - 1; i++) {
                        if (e[i].status != "dead") {
                            enemyController(e[i].label, "start");
                            break;
                        }
                    }*/
                    turn = "enemy";
                    enemyController(e[0].label, "start");
                }
                else {
                    turn = "player";
                }
            }
        }
        /*string temp = "";
        if (name.IndexOf("p") >= 0) {
            playerTurn = false;
            foreach (Enemy enemy in e) {
                enemy.isActing = true;
            }
        }
        else {
            foreach (string str in liveList) {
                if (str.IndexOf("e") >= 0 && str.IndexOf("dead") < 0) {
                    temp = str;
                }
            }
            if (name == temp) {
                playerTurn = true;
            }
            for (int i = 0; i < liveList.Length; i++) {
                if (liveList[i].IndexOf("e1_dead") >= 0 || liveList[i].IndexOf("e2_dead") >= 0) {
                    enemyTurn += 2;
                }
            }
            enemyTurn += 2;
        }*/
    }

    /*public void DeathEvent (int[] position, string name) {
        int count = 0;
        if (name.IndexOf("e") >= 0 && name != liveList[liveList.Length - 1]) {
            enemyTurn += 2;
        }
        for (int i = 0; i < liveList.Length; i++) {
            if (name == liveList[i]) {
                liveList[i] = name + "_dead";
            }
        }
        //board[position[0], position[1], 0] = "n";
        board.SetCharacterLabel(position[0], position[1], "n");
        foreach (string str in liveList) {
            if (str.IndexOf("p") >= 0) {
                if (str.IndexOf("dead") >= 0) {
                    count++;
                }
                else {
                    count = 0;
                }
                if (count >= MAX_PLAYER) {
                    SceneManager.LoadScene("EnemyWin");
                }
            }
            else {
                if (str.IndexOf("dead") >= 0) {
                    count++;
                }
                else {
                    count = 0;
                }
                if (count >= MAX_ENEMY) {
                    SceneManager.LoadScene("PlayerWin");
                }
            }
        }
    }*/

    public void DeathEvent (Character character) {
        character.status = "dead";
        board.SetCharacterLabel(character.position[0], character.position[1], "n");
        for (int i = 0, count = 0; i < MAX_PLAYER; i++) {
            if (p[i].status == "dead")
                count++;
            if(count == MAX_PLAYER)
                SceneManager.LoadScene("EnemyWin");
        }
        for (int i = 0, count = 0; i < MAX_ENEMY; i++) {
            if (e[i].status == "dead")
                count++;
            if (count == MAX_ENEMY)
                SceneManager.LoadScene("PlayerWin");
        }
    }

    /*public int[] GetTarget (int row, int column, int range, string direction) {
        string target = "";
        if (board.GetCharacter(row, column).IndexOf("p") >= 0)
            target = "e";
        else if (board.GetCharacter(row, column).IndexOf("e") >= 0)
            target = "p";
        else
            target = "";
        switch (direction) {
            case "up":
                for (int i = 1; i <= range; i++) {
                    if (board.GetCharacter(row - i, column).IndexOf(target) >= 0) {
                        return new int[2] { row - i, column };
                    }
                }
                break;
            case "down":
                for (int i = 1; i <= range; i++) {
                    if (board.GetCharacter(row + i, column).IndexOf(target) >= 0) {
                        return new int[2] { row + i, column };
                    }
                }
                break;
            case "left":
                for (int i = 1; i <= range; i++) {
                    if (board.GetCharacter(row, column - i).IndexOf(target) >= 0) {
                        return new int[2] { row, column - i };
                    }
                }
                break;
            case "right":
                for (int i = 1; i <= range; i++) {
                    if (board.GetCharacter(row, column + i).IndexOf(target) >= 0) {
                        return new int[2] { row, column + i };
                    }
                }
                break;
            default:
                break;
        }
        return new int[2] { -1, -1 };
    }*/

    private Character GetCharacter (int row, int column) {
        foreach (Character character in c) {
            if (character.label == board.GetCharacterLabel(row, column))
                return character;
        }
        return null;
    }

    private void BattleSettlement (ref Character target, Card aggr) {
        if (aggr.Type == target.pass.Type)
            target.HP = target.HP - Mathf.Max((aggr.Value - target.pass.Value), 0);
        else
            target.HP = target.HP - aggr.Value;
    }

    public bool PlayCard (int originRow, int originColumn, int targetRow, int targetColumn, Card card) {
        Character origin = GetCharacter(originRow, originColumn);
        Character target = GetCharacter(targetRow, targetColumn);
        if (card.Cost <= origin.stamina) {
            switch (card.Action) {
                case "attack":
                    if (Mathf.Abs(originRow - targetRow) + Mathf.Abs(originColumn - targetColumn) <= card.Range) {
                        if (target && target.type != origin.type) {
                            BattleSettlement(ref target, card);
                            origin.stamina -= card.Cost;
                            return true;
                        }
                        else if (!target) {
                            origin.stamina -= card.Cost;
                            return true;
                        }
                    }
                    break;
                case "defense":
                    if (target && target.label == origin.label) {
                        origin.pass = new Card(card);
                        origin.stamina -= card.Cost;
                        return true;
                    }
                    break;
                default:
                    break;
            }
        }
        return false;
    }

    /*public void CheckAggr () {
        int row = board.FindAggr()[0];
        int column = board.FindAggr()[1];
        if (row == -1 || column == -1)
            return;
        if (board.GetCharacter(row, column).IndexOf("e") >= 0) {
            foreach (Player player in p) {
                if (player.label == board.GetCharacter(board.GetAggr(row, column).TargetRow, board.GetAggr(row, column).TargetColumn)) {
                    player.HP = ComputeHP(player.HP, board.GetAggr(row, column), player.pass);
                    board.SetAggr(row, column, new Card());
                    return;
                }
            }
        }
        else if (board.GetCharacter(row, column).IndexOf("p") >= 0) {
            foreach (Enemy enemy in e) {
                if (enemy.label == board.GetCharacter(board.GetAggr(row, column).TargetRow, board.GetAggr(row, column).TargetColumn)) {
                    enemy.HP = ComputeHP(enemy.HP, board.GetAggr(row, column), enemy.pass);
                    board.SetAggr(row, column, new Card());
                    return;
                }
            }
        }
    }*/

}
