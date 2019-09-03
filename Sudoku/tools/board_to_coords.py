def main():
    board = []

    for _ in range(9):
        line = input()
        row = []
        idx = 1
        for idx in range(9):
            c = line[idx]
            num = ord(c) - ord('0')
            row.append(num if 1 <= num <= 9 else 0)
        board.append(row)

    for rowIdx, row in enumerate(board):
        for colIdx, val in enumerate(row):
            if val > 0:
                print("{} {} {}".format(rowIdx + 1, colIdx + 1, val))


if __name__ == "__main__":
    main()