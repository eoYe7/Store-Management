
<?php
$dsn = 'mysql:host=localhost;dbname=users';
$username = 'root';
$password = '';

try {
    $pdo = new PDO($dsn, $username, $password);
    
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    if (isset($_POST['showalllogfile'])) {
       // $filter = !empty($_POST['condition']) ? $_POST['condition'] : ""; // Assign the filter condition from the $_POST variable
    
        $query = "SELECT stocks.id as st_id, s.store_name as sname, i.name AS Item_name, qantity,
                        stock_type,st.user_name,st.transactionType,DATE_FORMAT(created_at,\"%Y/%m/%d\") as created_at,st.updated_at 
                FROM items i JOIN stocks ON i.id = stocks.item_id JOIN stores s 
                ON s.store_code = stocks.store_id join stockstransaction st on 
                             stocks.id =st.stock_id ORDER by st.created_at desc";
    
        // if (!empty($filter)) {
        //     $query .=  $filter; // Append the filter condition to the query
        // }
    
        $stmt = $pdo->query($query);
        $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);   
        foreach ($rows as $row) {
            $data = $row['st_id'] . "," . $row['sname'] . "," . $row['Item_name'] . "," . $row['stock_type'] . "," . $row['qantity'] . "," . $row['transactionType'] . "," . $row['user_name'] . "," . $row['created_at'] . "#";
            echo $data;
        }
    }
}catch(PDOException $ex){
    echo $ex->getMessage();

}