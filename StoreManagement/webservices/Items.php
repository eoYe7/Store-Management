<?php

$dsn = 'mysql:host=localhost;dbname=users';
$username = 'root';
$password = '';

try {
    $pdo = new PDO($dsn, $username, $password);
   

    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    if (isset($_POST['showallitems'])) {
        $stmt = $pdo->query("SELECT * FROM items");
        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $data = $row['id'] . "," . $row['name'] . "," . $row['Item_price']."#";
            echo $data;
        }
    }

    // Insert Item
    else if (isset($_POST['additem'])) {
        $stmt = $pdo->prepare("INSERT INTO items (name,Item_price) VALUES (:name,:price)");
        $stmt->bindParam(':name', $_POST['name']);
        $stmt->bindParam(':price', $_POST['price']);


        if ($stmt->execute()) {
            echo "Inserted successfully";
        } else {
            echo "Insertion failed";
        }
    }

    // Update Item
    else if (isset($_POST['updateitem'])) {
        $stmt = $pdo->prepare("UPDATE items SET name = :name item_price = :price WHERE id = :id");
        $stmt->bindParam(':name', $_POST['name']);
        $stmt->bindParam(':price', $_POST['price']);

        $stmt->bindParam(':id', $_POST['id']);

        if ($stmt->execute()) {
            echo "Updated successfully";
        } else {
            echo "Update failed";
        }
    }

    // Delete Item
    else if (isset($_POST['deleteitem'])) {
        $stmt = $pdo->prepare("DELETE FROM items WHERE id = :id");
        $stmt->bindParam(':id', $_POST['id']);

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