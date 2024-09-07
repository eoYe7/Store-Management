

<?php
$dsn = 'mysql:host=localhost;dbname=users';
$username = 'root';
$password = '';

try {
    $pdo = new PDO($dsn, $username, $password);
    
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    if (isset($_POST['showallStocks'])) {
        $filter = !empty($_POST['condition']) ? $_POST['condition'] : ""; // Assign the filter condition from the $_POST variable
    
        $query = "SELECT stocks.id, s.store_name, i.name AS Item_name, qantity, stock_type, transaction_date 
                  FROM items i 
                  JOIN stocks ON i.id = stocks.item_id 
                  JOIN stores s ON s.store_code = stocks.store_id   ";
    
        if (!empty($filter)) {
            $query .=  $filter; // Append the filter condition to the query
        }
    
        $stmt = $pdo->query($query);
        $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
    
        foreach ($rows as $row) {
            $data = $row['id'] . "," . $row['store_name'] . "," . $row['Item_name'] . "," . $row['stock_type'] . "," . $row['qantity'] . "," . $row['transaction_date'] . "#";
            echo $data;
        }
    }

    // Insert to stocks
   


    

    else if(isset($_POST['getitems'])){
        $stmt = $pdo->query("SELECT * FROM items");
        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $data = $row['id'] . "," . $row['name'] . "#";
            echo $data;
    }
}



else if (isset($_POST['addtostocks'])) {
    $store_id = $_POST['store_id'];
    $item_id = $_POST['item_id'];
    $stock_type = $_POST['stock_type'];
    $quantity = $_POST['quantity'];
    $user_name = $_POST['user_name'];

    $pdo->beginTransaction();

    try {
        $stmt = $pdo->prepare("INSERT INTO stocks (store_id, item_id, stock_type, qantity, transaction_date) VALUES (:store_id, :item_id, :stock_type, :quantity, NOW())");
        $stmt->bindParam(':store_id', $store_id);
        $stmt->bindParam(':item_id', $item_id);
        $stmt->bindParam(':stock_type', $stock_type);
        $stmt->bindParam(':quantity', $quantity);
        $stmt->execute();

        $stockId = $pdo->lastInsertId();

        $stmt2 = $pdo->prepare("INSERT INTO STOCKSTRANSACTION (stock_id, user_name, transactionType, created_at) VALUES (:stock_id, :user_name, 'insert', NOW())");
        $stmt2->bindParam(':stock_id', $stockId);
        $stmt2->bindParam(':user_name', $user_name);
        $stmt2->execute();

        $pdo->commit();
        echo "Inserted successfully";
    } catch (PDOException $ex) {
        $pdo->rollBack();
        echo "Insertion failed: " . $ex->getMessage();
    }
}

    
    // Update user
    else if (isset($_POST['updatestock'])) {
        try{   
            
                $pdo->beginTransaction();
                $stmt = $pdo->prepare("UPDATE stocks SET store_id = :store_id, item_id = :item_id, stock_type = :stock_type, qantity = :qantity WHERE id = :stock_id");
                $stmt->bindParam(':store_id', $_POST['store_id']);
                $stmt->bindParam(':item_id', $_POST['item_id']);
                $stmt->bindParam(':stock_type', $_POST['stock_type']);
                $stmt->bindParam(':qantity', $_POST['qantity']);
                $stmt->bindParam(':stock_id', $_POST['stock_id']);
        
                if ($stmt->execute()) {
                    $stmt2 = $pdo->prepare("INSERT INTO STOCKSTRANSACTION (stock_id, item_id, user_name, transactionType, updated_at) VALUES (:stock_id, :item_id, :user_name, 'update', NOW())");
                    $stmt2->bindParam(':stock_id', $_POST['stock_id']);
                    $stmt2->bindParam(':item_id', $_POST['item_id']);
                    $stmt2->bindParam(':user_name', $_POST['user_name']);
        
                    if ($stmt2->execute()) {
                        $pdo->commit();
                        echo "Updated successfully";
                    } else {
                        $pdo->rollBack();
                        echo "Insertion into STOCKSTRANSACTION failed";
                    }
                } else {
                    $pdo->rollBack();
                    echo "Update failed";
                }
            
        }catch(PDOException $ex){
            $pdo->rollBack();
        }
    }




    // Delete user
    else if (isset($_POST['deleteuser'])) {
        $stmt = $pdo->prepare("DELETE FROM user WHERE name = :name AND pass = :pass");
        $stmt->bindParam(':name', $_POST['name']);
        $stmt->bindParam(':pass', $_POST['pass']);

        if ($stmt->execute()) {
            echo "Deleted successfully";
        } else {
            echo "Deletion failed";
        }
    }
} catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}

?>



<form action=""></form>