
    function add(nam) {
        let a = Math.random();
        let res = document.getElementById('add '+ nam)
        res.innerHTML= ''
        res.innerHTML+= 'клуб добавлен'
        res.disabled="disabled"
        // res.id+=1
        document.getElementById('delete '+ nam).style.display= 'inline';

    }

    function del(nam) {
        document.getElementById('delete '+ nam).style.display= 'none';
        let res =  document.getElementById('add '+ nam)
        res.disabled= false
        res.innerHTML = 'Добавить клуб в список клубов участвующих в соревнованиях'

    }
   // alert("Whoo!! You have discovered");
    
