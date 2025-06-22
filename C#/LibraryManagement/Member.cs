public class Member {
    public int MemberId { get; set; }
    public string Name { get; set; }
    public List<string> BorrowedBooks { get; set; } // List of ISBNs of borrowed books

    public Member (int memberId, string name) {
        MemberId = memberId;
        Name = name;
        BorrowedBooks = new List<string>();
    }

    public override string ToString() {
        return $"Member ID: {MemberId}, Name: {Name}, Borrowed Books: {(BorrowedBooks.Count > 0 ? string.Join(", ", BorrowedBooks) : "None")}";
    }
}