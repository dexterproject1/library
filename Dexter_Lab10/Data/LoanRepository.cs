using library.Models;
using Npgsql;

namespace library.Data;

// Every SQL statement about loans lives in this one class.
public class LoanRepository
{
    public const int MaxOpenLoans = 3;             // the most books one member may have out at once

    private readonly NpgsqlDataSource _dataSource;

    public LoanRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    // Copies the current row into a LoanView. Positions 0 to 7 match every loan SELECT.
    private static LoanView ReadLoan(NpgsqlDataReader reader)
    {
        return new LoanView
        {
            LoanId = /* 1. the loan's id */,
            MemberId = /* 2. the member's id */,
            MemberName = /* 3. the member's name */,
            BookTitle = /* 4. the book's title */,
            LoanedOn = /* 5. the date it was lent */,
            ReturnedOn = /* 6. the date it came back, which may be NULL */,
            DaysAllowed = /* 7. how many days it may be kept */,
            Status = /* 8. Out, Overdue or Returned */
        };
    }

    // READ: every loan with its member and book, newest first.
    public async Task<List<LoanView>> GetAllAsync()
    {
        // 1. The SQL: three tables joined, and each loan's status worked out.
        // 2. An empty list, to fill.
        // 3. A command that will run the SQL.
        // 4. Run it, and get a reader for the rows.
        while (/* 5. there is another row to read */)
        {
            // 6. Turn the row into a LoanView and add it to the list.
        }

        // 7. Give the list back.
    }
    // BORROW: lend one book, unless the member already has MaxOpenLoans books out.
    // The check and the INSERT run inside one transaction: both happen, or neither does.
    public async Task<bool> BorrowAsync(long memberId, long bookId, int daysAllowed)
    {
        // 1. Open one connection to the database.
        // 2. Start a transaction on that connection.
        // 3. Lock this member's row: a second borrow for the same member waits until this one ends.
        // 4. Count the books this member has out.
        if (/* 5. the member is already at the limit */)
        {
            // 6. Undo the transaction: nothing is saved.
            // 7. Report that nothing was borrowed.
        }

        // 8. Record the loan. The database fills loaned_on with today's date.
        // 9. Make the transaction permanent.
        // 10. Report that the book was borrowed.
    }

}
