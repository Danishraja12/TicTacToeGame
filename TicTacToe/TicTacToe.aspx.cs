using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TicTacToe : System.Web.UI.Page
{
    private const string EMPTY_CELL = "-";
    private const string PLAYER_X = "X";
    private const string PLAYER_O = "O";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            boardContainer.Visible = false;
        }
        else
        {
            bool gameStarted = Session["Board"] != null;
            if (gameStarted)
            {
                boardContainer.Visible = true;
                InitializeBoardFromSession();
            }
        }
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        int boardSize = Convert.ToInt32(txtSize.Text);
        if (boardSize >= 3 && boardSize <= 10)
        {
            InitializeBoard(boardSize);
            boardContainer.Visible = true;
        }
        else
        {
            lblGameStatus.Text = "Please enter a board size between 3 and 10.";
        }
    }

    private void InitializeBoard(int size)
    {
        boardContainer.Controls.Clear();
        boardContainer.Visible = true;
        string[,] board = new string[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Button btn = new Button();
                btn.ID = $"btn_{i}_{j}";
                btn.Text = EMPTY_CELL;
                btn.Click += Cell_Click;
                btn.CssClass = "cell";
                boardContainer.Controls.Add(btn);

                board[i, j] = EMPTY_CELL;
            }
            boardContainer.Controls.Add(new LiteralControl("<br/>"));
        }
        Session["Board"] = board;
        Session["CurrentPlayer"] = PLAYER_X;
        Session["GameStatus"] = "In Progress";
        lblCurrentPlayer.Text = $"Current Player: {PLAYER_X}";
    }
    private void InitializeBoardFromSession()
    {
        string[,] board = (string[,])Session["Board"];
        int size = board.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Button btn = new Button();
                btn.ID = $"btn_{i}_{j}";
                btn.Text = board[i, j];
                btn.Click += Cell_Click;
                btn.CssClass = "cell";
                boardContainer.Controls.Add(btn);
            }
            boardContainer.Controls.Add(new LiteralControl("<br/>"));
        }
        lblCurrentPlayer.Text = $"Current Player: {(string)Session["CurrentPlayer"]}";
    }
    protected void Cell_Click(object sender, EventArgs e)
    {
        Button clickedCell = (Button)sender;
        string currentPlayer = (string)Session["CurrentPlayer"];
        int row = int.Parse(clickedCell.ID.Split('_')[1]);
        int col = int.Parse(clickedCell.ID.Split('_')[2]);
        string[,] board = (string[,])Session["Board"];
        if (board[row, col] == EMPTY_CELL && Session["GameStatus"].ToString() == "In Progress")
        {
            board[row, col] = currentPlayer;
            clickedCell.Text = currentPlayer;

            if (CheckWinState(currentPlayer))
            {
                Session["GameStatus"] = $"Player {currentPlayer} wins!";
                lblGameStatus.Text = Session["GameStatus"].ToString();
            }
            else if (CheckDraw())
            {
                Session["GameStatus"] = "It's a draw!";
                lblGameStatus.Text = Session["GameStatus"].ToString();
            }
            else
            {
                Session["CurrentPlayer"] = (currentPlayer == PLAYER_X) ? PLAYER_O : PLAYER_X;
                lblCurrentPlayer.Text = $"Current Player: {(string)Session["CurrentPlayer"]}";
            }
            boardContainer.Visible = true;
        }
    }
    private bool CheckWinState(string player)
    {
        string[,] board = (string[,])Session["Board"];
        int size = board.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            bool win = true;
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] != player)
                {
                    win = false;
                    break;
                }
            }
            if (win) return true;
        }
        for (int i = 0; i < size; i++)
        {
            bool win = true;
            for (int j = 0; j < size; j++)
            {
                if (board[j, i] != player)
                {
                    win = false;
                    break;
                }
            }
            if (win) return true;
        }
        bool diagonalWin = true;
        bool reverseDiagonalWin = true;
        for (int i = 0; i < size; i++)
        {
            if (board[i, i] != player)
            {
                diagonalWin = false;
            }
            if (board[i, size - 1 - i] != player)
            {
                reverseDiagonalWin = false;
            }
        }
        if (diagonalWin || reverseDiagonalWin) return true;

        return false;
    }
    private bool CheckDraw()
    {
        string[,] board = (string[,])Session["Board"];
        int size = board.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == EMPTY_CELL)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
