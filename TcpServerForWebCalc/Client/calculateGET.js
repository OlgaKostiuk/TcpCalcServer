function showResult()
{
	var x = form.xField.value;
	var y = form.yField.value;
	var op = form.opField.value;
	var res = calcN(Number(x),Number(y),op);
	form.resField.value = res;
}

function calculate(x,y,op)
{
	var result = 0;

	if(op == '+')
		result = x + y;
	else if(op == '-')
		result = x - y;
	else if(op == '*')
		result = x * y;
	else if(op == '/')
		result = x / y;		
		
	return result;
}

function calcN( a, b, op )
{
	if(op == '+')
		op = 'p';
	
	var req = "num1="+a+"&num2="+b+"&opr="+op;
	var rr = new XMLHttpRequest();
	rr.open('GET', 'http://localhost:9050?'+req, false);
	rr.send(null);
	//if(xmlhttp.status == 200)
	return rr.responseText;
}